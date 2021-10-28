 
# Alumni Network Portal API

##### Made by: 
- ###### Ville Hotakainen
- ###### Omar El Tokhy
- ###### Sergei Kaukiainen

###### Other project participants
- ###### Mikko Nikku

## Installation
```
git clone https://github.com/Vilhard/AlumniNetworkBackend.git
cd/AlumniNetworkBackend
start AlumniNetworkBackend.sln
```
## Usage
Navigate to ``appsettings.json`` and change defaultConnection string to point to your local MSSQL server
```
"ConnectionStrings": {
    "DefaultConnection": "Your local connection string"
  }
```
Navigate to Models/AlumniNetworkDbContext.cs and also change the `optionsBuilder.useSqlServer` to point to the same local MSSQL server
```
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Your local connection string");
        }
```
Open Package Manager Console and insert following commands:
`add-migration init`
`update-database`

These should create the alumni database for you.
> If something goes wrong during the migration...Be sure to check you have Microsoft.EntityFrameworkCore.Tools and Microsoft.EntityFrameworkCore.SqlServer installed

## Authentication
The endpoints of this project are protected with jwt token authentication. To be able to use the API with full functionality there are some setting up to be done. We are using service called Keycloak for authentication. We first need to create locally running keycloak server that will take care of the authentication for us. 

To start using keycloak we first need to create Docker container that will host the server for us. Good docker image to use is [jboss/keycloak](https://hub.docker.com/r/jboss/keycloak/) that has good documentation to to create the server. Or you can use the [Keycloak official documentation](https://www.keycloak.org/getting-started/getting-started-docker) with better step by step guidance.

After Keycloak is running on our local machine we need to change couple things on our API. 
```
 IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
                    {
                        var client = new HttpClient();
                        var keyuri = Configuration["TokenSecrets:KeyURI"];
                        //Retrieves the keys from keycloak instance to verify token
                        var response = client.GetAsync(keyuri).Result;
                        var responseString = response.Content.ReadAsStringAsync().Result;
                        var keys = JsonConvert.DeserializeObject<JsonWebKeySet>(responseString);
                        return keys.Keys;
                    },
                    ValidIssuers = new List<string>
                    {
                        Configuration["TokenSecrets:IssuerURI"]
                    },
```
Change `["TokenSecrets:KeyURI"]` to use `<Localhost>/auth/realms/<your-realm>`
Change`["TokenSecrets:KeyURI"]` to use  `<localhost>/auth/realms/<your-realm>/protocol/openid-connect/certs`

**Now everything is setup correctly to be able to the API to it's full function.**

# API Documentation

### Allowed HTTPs requests
- ##### ``PUT`` : To create resource
- ##### ``POST``: Update resource
- ##### ``GET``: Get resource or list of resources
- ##### ``DELETE``: Delete resource

### Server Responses
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
- Content-Type:application/json
- Authorization: Bearer ``your-token``

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

### `PUT` https://localhost:44344/api/user/id
Let's requesting user to make update on existing user by id
> string `id` value of the User to perfrom action with. Example: "abc123"

###### Headers
- Content-Type:application/json
- Authorization: Bearer ``your-token``

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
- Content-Type:application/json
- Authorization: Bearer ``your-token``

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

### `GET` https://localhost:44344/api/user/id
Returns the user information of a specific user by id
> string `id` value of the User to perfrom action with. Example: "abc123"

###### Headers
- Content-Type:application/json
- Authorization: Bearer ``your-token``

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
- Content-Type:application/json
- Authorization: Bearer ``your-token``

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
`401` Unauthorized - This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

### `POST` https://localhost:44344/api/group/id/join

###### Headers
- Content-Type:application/json
- Authorization: Bearer ``your-token``

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

`401` Unauthorized - This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

### `GET` https://localhost:44344/api/group
###### Headers
- Content-Type:application/json
- Authorization: Bearer ``your-token``

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
`401` Unauthorized -  This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

### `GET` https://localhost:44344/api/group/id
###### Headers
- Content-Type:application/json
- Authorization: Bearer ``your-token``

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
`401` Unauthorized - This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

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
- Content-Type:application/json
- Authorization: Bearer ``your-token``

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
`401` Unauthorized - This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

### `POST` https://localhost:44344/api/topic/id/join

###### Headers
- Content-Type:application/json
- Authorization: Bearer ``your-token``

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
`401` Unauthorized - This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid 

### `GET` https://localhost:44344/api/topic
###### Headers
- Content-Type:application/json
- Authorization: Bearer ``your-token``

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
`401` Unauthorized - This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

### `GET` https://localhost:44344/api/topic
###### Headers
- Content-Type:application/json
- Authorization: Bearer ``your-token``

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

`401` Unauthorized - This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

# Post

##### Post Attributes:
- id `(Number)`: unique identifier.
- Test `(string)`: Text of the post
- Timestamp `(Datetime)`: Time when post was created
- SenderId `(string)`: Id of the post creator
- SenderName `(string)`: Name of the post creator
- ReplyParentId `(Number)`: Identifies if post is a reply to another post
- TargetUserId `(string)`: Identifies if post is a direct message to specific user
- TargetGroupId `(Number)`: Identifies in which group the post is intendend to
- TargetTopicId `(Number)`: Identifies in which topic the post belongs to
- TargetEventId `(Number)`: Identifies in which event the post is intendend to
- TargetPosts `(List<Post>`: Identifies all reply's to specific post

##### Requests:
### `POST` https://localhost:44344/api/post
###### Headers
- Content-Type:application/json
- Authorization: Bearer ``your-token``

##### Request body
#
```json
{
    "text": "Sample post text",
    "targetTopic": 3,
    "members": [{
        "id": "2e7da74f-819f-4826-98e5-08238389ce9f"
    }]
}
````
> Members list is used to check id user can post to specific target

##### Response body
#
`201` Created - Post is created successfully
```json
{
    "text": "Sample post text",
    "senderId": "2e7da74f-819f-4826-98e5-08238389ce9f",
    "senderName": "john smith",
    "targetUserId": null,
    "timeStamp": "2021-10-28T12:54:11.7701261+03:00",
    "targetPosts": []
}
```
`401` Unauthorized - This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

`403` Forbidden - If member does not exist in the target member groups
```json
{
    "title": "Forbidden",
    "status": 403,
}
```
`404` Not Found - If user does not exist in the database
```json
{
    "title": "Not Found",
    "status": 404
}
```
`500` Server error - happens if members list is **missing** from the request!

### `PUT` https://localhost:44344/api/post/id
###### Headers
- Content-Type:application/json
- Authorization: Bearer ``your-token``

##### Request body
`200` OK - Post udpate was successfull
```json
{
    "id": 37,
    "text": "Replacing text",
    "members" : [{
        "id": "2e7da74f-819f-4826-98e5-08238389ce9f"
    }]
}
```
`400` Bad Request - if parameter id does not match the to be updated post `id`
```json
{
    "title": "Bad Request",
    "status": 400,
}
```
`401` Unauthorized - This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

`403` Forbidden - If the requesting user id not the creator of the post to be updated
```json
{
    "title": "Forbidden",
    "status": 403,
}
```
`404` Not Found - If the post does not exist in the database
```json
{
    "title": "Not Found",
    "status": 404
}
```

### `GET` https://localhost:44344/api/post
Returns a list of posts and topics in which the requesting user is a member of
###### Headers
- Content-Type:application/json
- Authorization: Bearer ``your-token``

##### Response body
#

`200` OK - Returns a list of posts from groups and topics for which the user is member of
```json
[
    {
        "id": 10,
        "text": "Harry Potter stuff is cool!",
        "timeStamp": "2021-12-12T19:30:00",
        "targetGroupId": null,
        "targetTopicId": 1,
        "targetPosts": []
    },
    {
        "id": 11,
        "text": "Yeah it's awesome!",
        "timeStamp": "2021-12-12T19:30:00",
        "targetGroupId": null,
        "targetTopicId": 1,
        "targetPosts": []
    },
    {
        "id": 14,
        "text": "The original trilogy was the best!",
        "timeStamp": "2021-12-12T19:30:00",
        "targetGroupId": null,
        "targetTopicId": 3,
        "targetPosts": []
    }
```
`204` No Content - Request is allowed but no content was found (User does not belong to any group or topic)

`401` Unauthorized -  This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

### `GET` https://localhost:44344/api/post/user
Returns a list of posts that were send to the requesting user
###### Headers
- Content-Type:application/json
- Authorization: Bearer ``your-token``

##### Response body
`200` OK - Returns a list of posts to requesting user
```json
[
    {
        "text": "Are we good on friday?",
        "senderId": "cef8bc1a-e523-4b61-bfdd-c851e68ee8b9",
        "senderName": "Michael Jordan",
        "targetUserId": "2e7da74f-819f-4826-98e5-08238389ce9f",
        "timeStamp": "2021-10-28T15:02:28.0116461",
        "targetPosts": []
    },
    {
        "text": "How are you?",
        "senderId": "cef8bc1a-e523-4b61-bfdd-c851e68ee8b9",
        "senderName": "Michael Jordan",
        "targetUserId": "2e7da74f-819f-4826-98e5-08238389ce9f",
        "timeStamp": "2021-10-28T15:02:11.8193724",
        "targetPosts": []
    }
]
```
`204` No Content - No posts were send to the requesting user

`401` Unauthorized - This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

### `GET` https://localhost:44344/api/post/user/id
Returns a list of posts that were send from specific user to the requesting user
###### Headers
- Content-Type:application/json
- Authorization: Bearer ``your-token``

##### Response body
`200` OK - Returns a list of posts
```json
[
    {
        "text": "See you soon!",
        "senderId": "cef8bc1a-e523-4b61-bfdd-c851e68ee232",
        "senderName": "Tommy Thompspon",
        "targetUserId": 2e7da74f-819f-4826-98e5-08238389ce9f,
        "timeStamp": "2021-10-28T15:02:28.0116461",
        "targetPosts": []
    },
    {
        "text": "Running late!",
        "senderId": "cef8bc1a-e523-4b61-bfdd-c851e68ee232",
        "senderName": "Tommy Thompson",
        "targetUserId": "2e7da74f-819f-4826-98e5-08238389ce9f",
        "timeStamp": "2021-10-28T15:02:11.8193724",
        "targetPosts": []
    }
]
```
`204` No Content - No posts were send from the specific user to the requesting user
`404` Not Found - If id does not match any user's with the same id in the datbase
```json
{
    "title": "Not Found",
    "status": 404
}
```
`401` Unauthorized - This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

### `GET` https://localhost:44344/api/post/group/id
Returns a list of posts that were send to the specific group `id`
###### Headers
- Content-Type:application/json
- Authorization: Bearer ``your-token``

##### Response body
`200` OK - Returns lis of posts from specific group
```json
[
    {
        "id": 16,
        "text": "Coding is fun!",
        "timeStamp": "2021-12-12T19:30:00",
        "targetGroupId": 1,
        "targetTopicId": null,
        "targetPosts": []
    },
    {
        "id": 17,
        "text": "I liked React more than Angular",
        "timeStamp": "2021-12-12T19:30:00",
        "targetGroupId": 1,
        "targetTopicId": null,
        "targetPosts": [
            18
        ]
    },
    {
        "id": 18,
        "text": "Yeah me too",
        "timeStamp": "2021-12-12T19:30:00",
        "targetGroupId": 1,
        "targetTopicId": null,
        "targetPosts": []
    }
]
```
`404` Not Found - If the group does not exist in the database
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
`401` Unauthorized - This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

### `GET` https://localhost:44344/api/post/topic/id
Returns a list of posts that were send to the specific topic `id`
###### Headers
- Content-Type:application/json
- Authorization: Bearer ``your-token``

##### Response body
`200` OK - Returns lis of posts from specific topic
```json
[
    {
        "id": 10,
        "text": "Harry Potter stuff is cool!",
        "timeStamp": "2021-12-12T19:30:00",
        "targetTopicId": 1,
        "targetGroupId": null,
        "targetEventId": null
    },
    {
        "id": 11,
        "text": "Yeah it's awesome!",
        "timeStamp": "2021-12-12T19:30:00",
        "targetTopicId": 1,
        "targetGroupId": null,
        "targetEventId": null
    }
]
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
`401` Unauthorized - This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

### `GET` https://localhost:44344/api/post/event/id
Returns a list of posts that were send to the specific event `id`
###### Headers
- Content-Type:application/json
- Authorization: Bearer ``your-token``

##### Response body
`200` OK - Returns lis of posts from specific event
```json
[
    {
        "id": 22,
        "text": "This party is going to be awesome!",
        "timeStamp": "2021-12-12T19:30:00",
        "targetTopicId": null,
        "targetEventId": 1
    },
    {
        "id": 23,
        "text": "Are we like full stack developers now?",
        "timeStamp": "2021-12-12T19:30:00",
        "targetTopicId": null,
        "targetEventId": 1
    }
]
```
`404` Not Found - If the event does not exist in the database
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
`401` Unauthorized -  This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

### `GET` https://localhost:44344/api/post/reply/id
Returns a list of posts that were send to the specific post `id` as a reply
###### Headers
- Content-Type:application/json
- Authorization: Bearer ``your-token``

##### Response body
`200` OK - Returns lis of post replys
```json
[
    {
        "text": "Feeling good!",
        "senderId": "2",
        "senderName": null,
        "targetUserId": null,
        "timeStamp": "2021-12-12T19:30:00",
        "targetPosts": []
    },
    {
        "text": "Today's going to be a good day!",
        "senderId": "3",
        "senderName": null,
        "targetUserId": null,
        "timeStamp": "2021-12-12T19:30:00",
        "targetPosts": []
    },
    {
        "text": "Yeah it's awesome!",
        "senderId": "3",
        "senderName": null,
        "targetUserId": null,
        "timeStamp": "2021-12-12T19:30:00",
        "targetPosts": []
    },
    {
        "text": "Experis is a nice place to work!",
        "senderId": "3",
        "senderName": null,
        "targetUserId": null,
        "timeStamp": "2021-12-12T19:30:00",
        "targetPosts": []
    }
]
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
`404` Not Found - If the post does not exist in the database
```json
{
    "title": "Not Found",
    "status": 404
}
```
`401` Unauthorized - This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

# Event

##### Event Attributes:
- id `(Number)`: unique identifier.
- Name `(string)`: Name of the event
- Description `(string)`: Description of the event
- AllowQuests `(boolean)`: Boolean value to check if event allows user's to join 
- BannerImg `(string)`: Picture for the event
- StartTime `(DatetTime)`: Identifies the starting time of the event
- EndTime `(DateTime)`: Identifies the ending time of the event
- TargetGroupId `(Number)`: Identifies in which group the event is intendend to
- TargetTopicId `(Number)`: Identifies in which topic the event belongs to
- CreatedById `(string)`: Identifies the user who created the event

##### Requests:
### `POST` https://localhost:44344/api/event
###### Headers
- Content-Type:application/json
- Authorization: Bearer ``your-token``

##### Request body
#
```json
{
  "name": "Placeholder event",
  "description": "This will hold information about the event",
  "allowGuests": true,
  "bannerImg": "https://randomurl.com",
  "startTime": "2021-10-28T15:37:49.529Z",
  "endTime": "2021-10-28T15:37:49.529Z",
  "targetTopicId": 1,
  "targetGroupId": 2
}
```
##### Response body
`201` Created - Event created successfully 
#
```json
{
    "lastUpdated": "0001-01-01T00:00:00",
    "name": "Placeholder event",
    "description": "This will hold information about the event",
    "allowGuests": true,
    "bannerImg": "https://randomurl.com",
    "startTime": "2021-10-28T15:37:49.529Z",
    "endTime": "2021-10-28T15:37:49.529Z",
    "targetTopicId": 1,
    "targetGroupId": 2
}
```
`401` Unauthorized - This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

`403` Forbidden - If the requesting user is not a member of group or topic where event is trying to be posted
```json
{
    "title": "Forbidden",
    "status": 403,
}
```
`404` Not Found - If the target topic or group does not exist in the database
```json
{
    "title": "Not Found",
    "status": 404
}
```
### `POST` https://localhost:44344/api/event/event-id/invite/group/group-id
###### Headers
- Content-Type:application/json
- Authorization: Bearer ``your-token``

##### Request body
`201` Created - Creates a new event group invitation for the event and group specified in the path
```json
{
  "lastUpdated": "2021-10-28T20:07:34.018Z",
  "id": 1,
  "name": "test-user",
  "description": "event-description",
  "allowGuests": true,
  "bannerImg": "some-image.jpg",
  "startTime": "2021-10-28T20:07:34.018Z",
  "endTime": "2021-10-28T20:07:34.018Z",
  "createdById": "1",
  "targetTopicId": null,
  "targetGroupId": 1
}
```
`401` Unauthorized - This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

`403` Forbidden - If the requesting user is not a member of group where invite is trying to be posted
```json
{
    "title": "Forbidden",
    "status": 403,
}
```
`404` Not Found - If the target event or group does not exist in the database
```json
{
    "title": "Not Found",
    "status": 404
}
```

### `DELETE` https://localhost:44344/api/event/event-id/invite/group/group-id
###### Headers
-Content-Typeapplication-json
-Authorization: Bearer `your-token`

##### Response body
`200` Ok - Deletes a group event invitation record specified in the path
```json
{
  "lastUpdated": "2021-12-12T23:30:00",
  "id": 2,
  "name": "Experis Graduation",
  "description": "Experis Graduation party",
  "allowGuests": true,
  "bannerImg": "http://experisacademy.no/storage/images/fbook.jpg",
  "startTime": "2021-10-30T19:30:00",
  "endTime": "2021-10-30T23:30:00",
  "createdById": "2",
  "targetTopicId": null,
  "targetGroupId": 2
}
```
`401` Unauthorized - This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

`403` Forbidden - If the requesting user is not a member of group where invite is trying to be deleted
```json
{
    "title": "Forbidden",
    "status": 403,
}
```
`404` Not Found - If the target event or group does not exist in the database
```json
{
    "title": "Not Found",
    "status": 404
}
```

### `POST` https://localhost:44344/api/event/event-id/invite/topic/topic-id
###### Headers
- Content-Type:application/json
- Authorization: Bearer ``your-token``

##### Request body
`201` Created - Creates a new event topic invitation for the event and topic specified in the path
```json
{
  "lastUpdated": "2021-10-28T20:07:34.018Z",
  "id": 1,
  "name": "test-user",
  "description": "event-description",
  "allowGuests": true,
  "bannerImg": "some-image.jpg",
  "startTime": "2021-10-28T20:07:34.018Z",
  "endTime": "2021-10-28T20:07:34.018Z",
  "createdById": "1",
  "targetTopicId": 1,
  "targetGroupId": null
}
```
`401` Unauthorized - This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

`403` Forbidden - If the requesting user is not a member of topic where invite is trying to be posted
```json
{
    "title": "Forbidden",
    "status": 403,
}
```
`404` Not Found - If the target topic or event does not exist in the database
```json
{
    "title": "Not Found",
    "status": 404
}
```

### `DELETE` https://localhost:44344/api/event/event-id/invite/topic/topic-id
###### Headers
-Content-Typeapplication-json
-Authorization: Bearer `your-token`

##### Response body
`200` Ok - Deletes a topic event invitation record specified in the path
```json
{
  "lastUpdated": "2021-12-12T23:30:00",
  "id": 2,
  "name": "test-topic",
  "description": "test-description",
  "allowGuests": true,
  "bannerImg": "example.jpg",
  "startTime": "2021-10-30T19:30:00",
  "endTime": "2021-10-30T23:30:00",
  "createdById": "2",
  "targetTopicId": 2,
  "targetGroupId": null
}
```
`401` Unauthorized - This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

`403` Forbidden - If the requesting user is not a member of topic where invite is trying to be deleted
```json
{
    "title": "Forbidden",
    "status": 403,
}
```
`404` Not Found - If the target topic or event does not exist in the database
```json
{
    "title": "Not Found",
    "status": 404
}
```

### `POST` https://localhost:44344/api/event/event-id/invite/user/user-id
###### Headers
- Content-Type:application/json
- Authorization: Bearer ``your-token``

##### Request body
`201` Created - Creates a new event user invitation for the event and user specified in the path
```json
{
  "lastUpdated": "2021-10-28T20:07:34.018Z",
  "id": 1,
  "name": "test-user",
  "description": "event-description",
  "allowGuests": true,
  "bannerImg": "some-image.jpg",
  "startTime": "2021-10-28T20:07:34.018Z",
  "endTime": "2021-10-28T20:07:34.018Z",
  "createdById": "1",
  "targetTopicId": 1,
  "targetGroupId": null
}
```
`401` Unauthorized - This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

`403` Forbidden - If the requesting user is not a creator of the event the request is going to result in forbidden result
```json
{
    "title": "Forbidden",
    "status": 403,
}
```
`404` Not Found - If the target user or event does not exist in the database
```json
{
    "title": "Not Found",
    "status": 404
}
```

### `DELETE` https://localhost:44344/api/event/event-id/invite/user/user-id
###### Headers
-Content-Typeapplication-json
-Authorization: Bearer `your-token`

##### Response body
`200` Ok - Deletes a user event invitation record specified in the path
```json
{
  "lastUpdated": "2021-12-12T23:30:00",
  "id": 2,
  "name": "test-topic",
  "description": "test-description",
  "allowGuests": true,
  "bannerImg": "example.jpg",
  "startTime": "2021-10-30T19:30:00",
  "endTime": "2021-10-30T23:30:00",
  "createdById": "2",
  "targetTopicId": 2,
  "targetGroupId": null
}
```
`401` Unauthorized - This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

`403` Forbidden - If the requesting user is not a creator of the event the request is going to result in forbidden result
```json
{
    "title": "Forbidden",
    "status": 403,
}
```
`404` Not Found - If the target user or event does not exist in the database
```json
{
    "title": "Not Found",
    "status": 404
}
```
### `PUT` https://localhost:44344/api/event/id
###### Headers
-Content-Type: application-json
-Authorization: Bearer `your-token`

#### Request body
`204` Ok- Updates an event record specified in the path
```json
{
  "id": 1,
  "lastUpdated": "2021-10-28T21:32:43.751Z",
  "name": "test",
  "description": "test-description",
  "allowGuests": true,
  "bannerImg": "example.jpg",
  "startTime": "2021-10-28T21:32:43.751Z",
  "endTime": "2021-10-28T21:32:43.751Z"
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
`401` Unauthorized - This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

`403` Forbidden - If the requesting user is not a creator of the event the request is going to result in forbidden result
```json
{
    "title": "Forbidden",
    "status": 403,
}
```
`404` Not Found - If the event does not exist in the database
```json
{
    "title": "Not Found",
    "status": 404
}
```

### `POST` https://localhost:44344/api/event/event-id/rsvp
###### Headers
- Content-Type:application/json
- Authorization: Bearer ``your-token``

##### Request body
`201` Created - Creates a new RSVP record for the event specified in the path
```json
{
  "lastUpdated": "2021-10-28T21:50:36.489Z",
  "guestCount": 7,
  "userId": "2",
  "eventId": 3
}
```
`401` Unauthorized - This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

`403` Forbidden - If the requesting user is not a creator of the event the request is going to result in forbidden result
```json
{
    "title": "Forbidden",
    "status": 403,
}
```
`404` Not Found - If the target event does not exist in the database
```json
{
    "title": "Not Found",
    "status": 404
}
```

### `GET` https://localhost:44344/api/event
###### Headers
- Content-Type:application/json
- Authorization: Bearer ``your-token``

##### Response body
`200` OK - Returns lis of events in topics and groups which the requesting user is a member of
```json
[
    {
        "lastUpdated": "2021-12-12T23:30:00",
        "id": 1,
        "name": "Noroff Graduation",
        "description": "Noroff Graduation party",
        "allowGuests": true,
        "bannerImg": "https://keystoneacademic-res.cloudinary.com/image/upload/element/12/121913_121910_Noroff-logo.png",
        "startTime": "2021-12-12T19:30:00",
        "endTime": "2021-12-12T23:30:00",
        "createdById": "2",
        "targetTopicId": 1,
        "targetGroupId": 2
    },
    {
        "lastUpdated": "2021-12-12T23:30:00",
        "id": 2,
        "name": "Experis Graduation",
        "description": "Experis Graduation party",
        "allowGuests": true,
        "bannerImg": "http://experisacademy.no/storage/images/fbook.jpg",
        "startTime": "2021-12-12T19:30:00",
        "endTime": "2021-12-12T23:30:00",
        "createdById": "2",
        "targetTopicId": 3,
        "targetGroupId": 2
    },
    {
        "lastUpdated": "2021-12-12T23:30:00",
        "id": 3,
        "name": "Just A Party",
        "description": "Just A Party for people who want to get wasted for no reason",
        "allowGuests": true,
        "bannerImg": "https://cdn.pixabay.com/photo/2019/11/14/03/22/party-4625237_960_720.png",
        "startTime": "2021-12-12T19:30:00",
        "endTime": "2021-12-12T23:30:00",
        "createdById": "1",
        "targetTopicId": 4,
        "targetGroupId": 2
    }
]
```
`204` No Content - Topics or groups  which the requesting user is member of does not have events

`401` Unauthorized - This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

### `GET` https://localhost:44344/api/event/attendees/id
###### Headers
- Content-Type:application/json
- Authorization: Bearer ``your-token``

##### Response body
`200` OK - Returns list of members that are attending `id` event
```json
[
    {
        "id": "2",
        "name": "Hermione Granger",
        "username": "TheGirlWhoReads",
        "picture": "https://data.topquizz.com/distant/quizz/big/6/0/9/9/219906_527364a914.jpg",
        "status": "Reading books",
        "bio": "I like reading",
        "funFact": "I secretly like Ron"
    },
    {
        "id": "3",
        "name": "Ron Weasley",
        "username": "TheBoyWhoGinger",
        "picture": "https://data.topquizz.com/distant/quizz/big/4/5/4/9/239454_33e86ba948.jpg",
        "status": "Eating candy",
        "bio": "Huh?",
        "funFact": "I secretly like Hermione"
    }
]
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

`401` Unauthorized - This happens when bearer token  (`your-token`) is invalid, so be sure to check that it is valid

`404` Not Found - If the event does not exist in the database
```json
{
    "title": "Not Found",
    "status": 404
}
```
## License

MIT

