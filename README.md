# Rating Browser

This project is a proof of concept for using .NET Forms to consume RESTful APIs.

Project shows a implementation of browsing ratings that are fetched from a API inside the company's network.

## Configuration 

Settings.Example, depicts a example of what is needed to get the project to work.

The two following variables are used:
* ITAPIURL, that defines the base URL for your API. 
* ITAPIKEY, that defines the KEY used to pass to the API for autohentication.

## Specification of the web service

This proof of concept used the JSON communication format for the source Web API.

Ratings have the following specification:

GET $BASE$/ratings (Expect 200 OK):
```
{
  "count": 1038,
  "data": {
    "ratings": [
    {
      "comment": "Issue was fixed quickly :)",
      "created": "Wed, 11 Jun 2014 13:35:41 GMT",
      "fullname": "Name of Employee",
      "id": 1,
      "jirakey": "TIC-14877",
      "stars": 5,
      "updated": "Wed, 11 Jun 2014 13:35:41 GMT",
      "username": "name.of.emp@company.com"
    },
    ...
    ,
	]
  },
  "status": "success",
  "total_closed": 0
}
```