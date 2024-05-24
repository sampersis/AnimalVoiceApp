import json
import requests

successful_response = [200, 201, 202, 203, 204, 205, 206, 207, 208, 226]



def http_get_all():
    base_url = "https://localhost:7082/api/Animals"

    http_get_response = requests.get(url = base_url, verify=False)
    
    http_response_status_code = http_get_response.status_code
    
    if(http_response_status_code in successful_response):
        data = http_get_response.json()

        print(f"\nNumber of records retrieved: {len(data)}\n")

        record = 1
        result = ""
        for item in data:
            result = result + f"Record #{record}\n"
            print("Record #{}".format(record))
            record += 1
            print(item)
            result = result + json.dumps(item) + "\n"
            for key,value in item.items():
                print("{}: {}".format(key,value))
                result = result + f"{key}: {value}\n"
            print("\n")
            result = result + "\n"
    else:
        result = f"HTTP Response Status Code {http_response_status_code} was returned. Please consult RFC9110 https://httpwg.org/specs/rfc9110.html#overview.of.status.codes for more information\n"
         
    return result
    
    