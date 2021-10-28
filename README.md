
# Alumni Network Portal API

Alumni portal api description

## Allowed HTTPs requests
##### ``PUT`` : To create resource
##### ``POST``: Update resource
##### ``GET``: Get resource or list of resources
##### ``DELETE``: Delete resource

## Server Responses
 - ##### 200 `OK` - the request was successful.


 - ##### 201 `Created` - the request was successful and a resource was created.


 - ##### 204 `No Content` - the request was successful but there is no representation to return (i.e. the response is empty).
 

 - ##### 400 `Bad Request` - the request could not be understood or was missing required parameters.
 

- ##### 401 `Unauthorized` - authentication failed or user doesn't have permissions for requested operation. 
 

- ##### 403 `Forbidden` - access denied.


- ##### 404 `Not Found` - resource was not found.


- ##### 405 `Method Not Allowed` - requested method is not supported for resource.

# API Endpoints

# User
Tähän jotain kuvausta

##### User Attributes:
- id `(string)`: unique identifier.
- Name `(string)`: name of user
- Username `(string)`: username of user
- Picture `(string)`: url of user picture
- Status `(string)`: User's work status message
- Bio `(string)`: User's personal description
- FunFact `(string)`: User's personal fun fact about them.

##### Requests:
### `POST` https://localhost:44344/api/user
###### Headers
Content-Type:application/json
Authorization: Bearer ``your-token``
###### Request Body
#

```json
{
  "id": "1",
  "name": "John",
  "username": "JohnSmith",
  "email": "smith@email.com",
  "picture": "https:/pictureofurl.com",
  "status": "Testing endpoints",
  "bio": "Born to test",
  "funfact": "Not actually real person"
}
```

##### Response:
`201` User created successfully

```json
{
    "id": "1",
  "name": "John",
  "username": "JohnSmith",
  "email": "smith@email.com",
  "picture": "https:/pictureofurl.com",
  "status": "Testing endpoints",
  "bio": "Born to test",
  "funfact": "Not actually real person"
}
```

`401` Unauthorized This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

### `PUT` https://localhost:44344/api/user/`id`
Let's requesting user to make update on existing user by id
> string `id` value of the User to perfrom action with. Example: "abc123"

###### Headers
Content-Type:application/json
Authorization: Bearer ``your-token``
###### Request Body
#

```json
{
  "id": "1",
  "status": "Stopped Testing endpoints",
  "funfact": "Becoming a person"
}
```
##### Reponse
`200` OK
 ```json
{
  "id": "1",
  "name": "John",
  "username": "JohnSmith",
  "email": "smith@email.com",
  "picture": "https:/pictureofurl.com",
  "status": "Stopped Testing endpoints",
  "bio": "Born to test",
  "funfact": "Becoming a person"
}
```
`404` Not Found
> user with parameter `id` does not exist in the database
```json
{
    "title": "Not Found",
    "status": 404
}
```
`401` - Unauthorized 
> This happens when bearer token is invalid (`your-token`) and when requesting user id does not match to the parameter `id`. (Cannot update someone else's information!)


### `GET` https://localhost:44344/api/user
Returns the user information of the **requesting** user
> Requesting user is authenticated with the bearer token that is passed through authorization header

###### Headers
Content-Type:application/json
Authorization: Bearer ``your-token``
##### Reponse
`200` OK

```json
{
    "id": "1",
  "name": "John",
  "username": "JohnSmith",
  "email": "smith@email.com",
  "picture": "https:/pictureofurl.com",
  "status": "Testing endpoints",
  "bio": "Born to test",
  "funfact": "Not actually real person"
}
```
`404` Not Found
> Requesting user does not exist in the database
```json
{
    "title": "Not Found",
    "status": 404
}
```
`401` Unauthorized This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

### `GET` https://localhost:44344/api/user/`id`
Returns the user information of a specific user by id
> string `id` value of the User to perfrom action with. Example: "abc123"

###### Headers
Content-Type:application/json
Authorization: Bearer ``your-token``
##### Reponse
`200` OK
 ```json
 {
    "id": "abc123",
    "name": "Harry Potter",
    "username": "TheBoyWhoLives",
    "picture": "https//urltopicture.com",
    "status": "Working on new spells",
    "bio": "Fighting against dark forces since day one",
    "funFact": "I can speak parseltongue"
}
 ```
`404` Not Found
> Requesting user does not exist in the database
```json
{
    "title": "Not Found",
    "status": 404
}
```
`401` Unauthorized This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

# Group
Tähän jotain kuvausta

##### Group Attributes:
- id `(Number)`: unique identifier.
- Name `(string)`: name of the group
- Description `(string)`: Description of group
- isPrivate `(boolean)`: Boolean value that determines private group
- Members `(List<string>)`: Id's of all the members that are in the group

##### Requests:
### `POST` https://localhost:44344/api/group
> Creating a group adds the creator to become the first member of the group

###### Headers
Content-Type:application/json
Authorization: Bearer ``your-token``
###### Request Body
#

```json
{
  "name": "John's Birthday Party",
  "description": "Come and celebrate John's 30th birthday!",
  "isPrivate": true
}
```

##### Response:
`201` User created successfully

```json
{
    "id": 1,
    "name": "John's Birthday Party",
    "description": "Come and celebrate John's 30th birthday!",
    "isPrivate": true,
    "members": [{
        "1",
    }]
}
```
`401` Unauthorized This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

### `POST` https://localhost:44344/api/group/`id`/join

###### Headers
Content-Type:application/json
Authorization: Bearer ``your-token``
###### Request Body
#

##### Response:
`200` OK User added to group successfully
```json
{
    "id": 3,
    "name": "Comicbook Nerds",
    "members": [
        "b3b51c4f-8b29-4e3b-a0ba-8ec29200098b",
        "abc123"
    ]
}
```
`404` Not Found - Happens if does not exist in the database when trying to join a group

`403` Forbidden - If the group for which the membership record is being created is private, then only currrent members of the group may create group member records for that group.

`401` Unauthorized This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

### `GET` https://localhost:44344/api/group
###### Headers
Content-Type:application/json
Authorization: Bearer ``your-token``
##### Response:
`200` OK - Returns a list of groups that are not private and requesting user is a member of
```json
[
    {
        "id": 3,
        "name": "Comicbook Nerds",
        "description": "Group for Everyone who loves Comicbooks!",
        "isPrivate": false,
        "members": [
            "2a5b9ffe-cd6d-41d1-a376-8c9eff266935"
        ]
    },
    {
        "id": 4,
        "name": "Movie Fans",
        "description": "Group for Everyone who loves movies!",
        "isPrivate": true,
        "members": [
            "abc123"
        ]
    }
]
```
`401` Unauthorized This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

### `GET` https://localhost:44344/api/group/`id`
###### Headers
Content-Type:application/json
Authorization: Bearer ``your-token``
##### Response:
`200` OK - Returns the group that corresponds to the paramater `id`
```json
{
    "id": 3,
    "name": "Comicbook Nerds",
    "description": "Group for Everyone who loves Comicbooks!",
    "isPrivate": false,
    "members": [
        "2a5b9ffe-cd6d-41d1-a376-8c9eff266935",
        "abc123"
    ]
}
```
`403` Forbidden - If group is private and requesting user is not a member of the group
```json
{
    "title": "Forbidden",
    "status": 403,
}
```
`404` Not Found - if the group does not exist in the database
```json
{
    "title": "Not Found",
    "status": 404,
}
```
`400` Bad Request - If the parameter is not valid
```json
{
    "title": "One or more validation errors occurred.",
    "status": 400,
    "errors": {
        "id": [
            "The value 's' is not valid."
        ]
    }
}
```
`401` Unauthorized This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

# Topic
Tähän jotain kuvausta

##### Topic Attributes:
- id `(Number)`: unique identifier.
- Name `(string)`: name of the Topic
- Description `(string)`: Description of the topic

##### Requests:
### `POST` https://localhost:44344/api/topic
> Creates new Topic with name and description

###### Headers
Content-Type:application/json
Authorization: Bearer ``your-token``
###### Request Body
```json
{
    "name": "Awesome stuff!",
    "desciption": "All these are awesome"
}
```
##### Response:
`201` Created - Topic created successfully
```json
{
    "name": "Awesome stuff!",
    "desciption": "All these are awesome"
}
```
`404` Not Found - If the creator of the topic does not exist in the database
```json
{
    "title": "Not Found",
    "status": 404
}
```
`401` Unauthorized This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

### `POST` https://localhost:44344/api/topic/`id`/join

###### Headers
Content-Type:application/json
Authorization: Bearer ``your-token``
###### Request Body
#

##### Response:
`200` OK - User added to topic user list successfully
```json
{
    "id": 1,
    "name": "Harry Potter",
    "users": [
        {
            "id": "2a5b9ffe-cd6d-41d1-a376-8c9eff266935",
            "username": "builderbob"
        }
    ]
}
```
`404` Not Found - Happens if topic or requesting user does not exist in the database
```json
{
    "title": "Not Found",
    "status": 404
}
```

`400` Forbidden - If the `id` parameter is not a valid
```json
{
    "title": "One or more validation errors occurred.",
    "status": 400,
    "errors": {
        "id": [
            "The value 's' is not valid."
        ]
    }
}
```
`401` Unauthorized This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid 

### `GET` https://localhost:44344/api/topic
###### Headers
Content-Type:application/json
Authorization: Bearer ``your-token``
##### Response:
`200` OK - Returns list of all topics
```json
[
    {
        "id": 1,
        "name": "Harry Potter",
        "description": "Hogwarts stuff"
    },
    {
        "id": 2,
        "name": "Lord of the rings",
        "description": "Middle earth stuff"
    },
    {
        "id": 3,
        "name": "Star Wars",
        "description": "Space stuff"
    }
```
`401` Unauthorized This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

### `GET` https://localhost:44344/api/topic
###### Headers
Content-Type:application/json
Authorization: Bearer ``your-token``
##### Response:
`200` OK - Returns specific topic that corresponds to the parameter id

```json
{
    "id": 1,
    "name": "Harry Potter",
    "description": "Hogwarts stuff"
}
```
`404` Not Found - If the topic does not exist in the database
```json
{
    "title": "Not Found",
    "status": 404
}
```

`400` Bad Request - If the parameter is not valid
```json
{
    "title": "One or more validation errors occurred.",
    "status": 400,
    "errors": {
        "id": [
            "The value 's' is not valid."
        ]
    }
}
```

`401` Unauthorized This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

## License

MIT

