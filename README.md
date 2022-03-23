# ScuffChat
Chat app in C# and ~~MS SQL~~ postgreSQL. Supports user accounts and direct messages.
![image](https://user-images.githubusercontent.com/27998891/159696869-7d458bc6-01d4-4966-86d0-5934138e6508.png)


# Setup

## Client
Set the server IP in launcher, then proceed to log in or register.

## Host
* install postgreSQL 
* make a new database called "chat"
* allow access to the db from other IPs
* run queries from the sql folder in order:
>1. tables.sql
>2. procedures.sql
>3. functions.sql
>4. perms.sql


### to do:
- ~~error message when connection fails~~
- ~~pause refreshing while hovering over message list~~
