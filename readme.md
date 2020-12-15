### Usage
- Run `Update-Database`
- Use `POST api/Family` to create a full family tree
- Add children, parent and grandparent using 
    - `POST api/Family/Child`
    - `POST api/Family/Parent`
    - `POST api/Family/Grandparent`
- Get the `PersonID` (and print the family tree) by using `GET api/Family` 
- There is no Family-Person relationship (as the requirement do not specify this), therefore this API only support 1 Family tree, if a family tree reach an faulty state (by a bug), or create a new family tree, reset the family tree using `DELETE api/Family` endpoint
- Ancestor/root can be called on `POST api/Family/Parent`, creating a new ancestor/root, but can not be called on `POST api/Family/Grandparent`
- Spouse can not be called on `POST api/Family/Parent` as well as `POST api/Family/Grandparent`
- SQLite 3.24.0 with SQLiteStudio 3.2.1

### Sample requests
#### Endpoint: `/Family`
```
{
    "Ancestor": {
        "FirstName": "Andrew",
        "LastName": "Turtle",
        "Gender": "Male",
        "DateOfBirth": "1835/8/15",
        "DateOfDeath": "1890/4/12",
        "Spouse": {
            "FirstName": "Mary",
            "LastName": "Withmore",
            "Gender": "Female",
            "DateOfBirth": "1830/12/10",
            "DateOfDeath": "1896/11/19"
        },
        "Childrens": [
            {
                "FirstName": "David",
                "LastName": "Turtle",
                "Gender": "Male",
                "DateOfBirth": "1859/6/2",
                "DateOfDeath": "1922/11/12",
                "Spouse": {
                    "FirstName": "Mary",
                    "LastName": "Tsaha",
                    "Gender": "Female",
                    "DateOfBirth": "1860/10/10",
                    "DateOfDeath": "1924/10/18"
                },
                "Childrens": [
                    {
                        "FirstName": "May",
                        "LastName": "Turtle",
                        "Gender": "Female",
                        "DateOfBirth": "1883/2/16",
                        "DateOfDeath": "1940/11/2"
                    },
                    {
                        "FirstName": "Eliza",
                        "LastName": "Turtle",
                        "Gender": "Female",
                        "DateOfBirth": "1883/2/16",
                        "DateOfDeath": "1938/10/3"
                    },
                    {
                        "FirstName": "Dave",
                        "LastName": "Turtle",
                        "Gender": "Male",
                        "DateOfBirth": "1884/4/16",
                        "DateOfDeath": "1945/9/15",
                        "Spouse": {
                            "FirstName": "Anna",
                            "LastName": "Mackenzi",
                            "Gender": "Female",
                            "DateOfBirth": "1883/2/16",
                            "DateOfDeath": "1944/4/3"
                        },
                        "Childrens": [
                            {
                                "FirstName": "Ahn",
                                "LastName": "Turtle",
                                "Gender": "Female",
                                "DateOfBirth": "1908/5/16",
                                "DateOfDeath": "1990/1/3"
                            }
                        ]
                    }
                ]
            },
            {
                "FirstName": "Mike",
                "LastName": "Turtle",
                "Gender": "Male",
                "DateOfBirth": "1861/7/6",
                "DateOfDeath": "1923/7/18"
            }
        ]
    }
}
```
#### Endpoint: `/Child` or `/Parent` or `/Grandparent`
```
{
    "ParentId/ChildId/GrandchildId": PERSON_ID,
    "Child/Parent/Grandparent": {
        "FirstName": "Mike",
        "LastName": "Turtle",
        "Gender": "Male",
        "DateOfBirth": "1889/3/31",
        "DateOfDeath": "1962/9/4"
    }
}
```