import sys
import os
import signal
import warnings
import logging
import datetime
from urllib.error import HTTPError
from termcolor import colored, cprint
import httpget

# Function to clear the screen
def wipe_screen():
    os.system('cls' if os.name == 'nt' else 'clear')

# Function to handle SIGINT and SIGTERM signals  
def signal_handler(signum, frame):
    wipe_screen()
    signal_name = signal.Signals(signum).name
    print(colored(f"\n{__name__}: {datetime.datetime.now()}: api testing app terminated by signal {signal_name}\n","red"))
    sys.exit()
    
def logger(log_file_name, http_operation, log_text):

    horozontal_line = '-' * 80
    horozontal_line = horozontal_line + "\n"
    log_file = open(log_file_name, "a")
    log_file.write(horozontal_line) 
    log_file.write(f"{datetime.datetime.now()} - {__name__} - {http_operation}\n")
    log_file.write(horozontal_line) 
    log_file.write(log_text)
    log_file.write(horozontal_line) 
    log_file.close()
    

    
# def create_logger(log_file_name):
    # logger = logging.getLogger(__name__)
    # logger.addHandler(logging.StreamHandler())
    # logging.basicConfig(filename=log_file_name, encoding='utf-8', level=logging.DEBUG)
    # logger.debug('\n-------------------------------------------------------------------------------------')
    # logger.debug(f' Logging started {datetime.datetime.now()}')
    # logger.debug('-------------------------------------------------------------------------------------\n')
   
    # return logger

def print_menu():
    menu_options = ["Menu for testing Animal API", "1. GET Request", "2. Get Request by ID", "3. Create Request", "4. Update Request", "5. Delete Request"]
    menu_option_colors = ["magenta","green","light_green","blue","yellow","red"]
    
    color_index = 0
    for menu_option in menu_options:
        if (color_index == 0 ):
            menu_option_text = colored(menu_option,menu_option_colors[color_index],"on_yellow")
        else:
            menu_option_text = colored(menu_option,menu_option_colors[color_index])
            
        print(menu_option_text)
        color_index += 1
        

def menu_option_select():
    accepted_values = "12345"

    print_menu()
    
    try:
        menu_selection = input("\n Select an option (1-5) or any other key to quit: ")
        
        if len(menu_selection) == 1 and (menu_selection in accepted_values):
            return menu_selection
        else:
            return 'X'
    except EOFError as eof_err:
        wipe_screen()
        print(colored(f"\n{__name__}: {datetime.datetime.now()}: api testing app terminated EOFError\n","red", attrs=["reverse","blink"]))
        sys.exit()

def animal_api_testing_app():
    log_file_name = "logfile.log"
        
    warnings.filterwarnings("ignore")
    signal.signal(signal.SIGINT,signal_handler)
    signal.signal(signal.SIGTERM,signal_handler)
    #logger = create_logger(log_file_name)
    wipe_screen()
    
    while True:
        try:
            option_selected = menu_option_select()
        
            if (option_selected == 'X'):
                wipe_screen()
                print(colored(f"\n{__name__}: {datetime.datetime.now()}: api testing app terminated by the user!\n","yellow", attrs=["reverse"]))
                sys.exit(0)
            else:
                print (colored(option_selected,"magenta","on_blue"))
                
                match option_selected:
                    case '1':
                        try:
                            data = httpget.http_get_all()
                            logger(log_file_name, "HTTP GET ALL", data)
                        except HTTPError as http_error:
                            logger(log_file_name, "HTTP GET ALL", f"HTTP GET ALL failed: HTTP Error Code : {http_error.code}")
                        
                    case '2':
                        pass
                        
                    case '3':
                        pass
                        
                    case '4':
                        pass
                        
                    case'5':
                        pass
                    
        except RuntimeError as exception_err:
            wipe_screen()
            print(colored(f"\n{__name__}: {datetime.datetime.now()}: api testing app teminated received exception: {exception_err}\n","red", attrs=["reverse","blink"]))
            sys.exit(1)

          
animal_api_testing_app()