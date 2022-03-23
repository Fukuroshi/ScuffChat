# ScuffChat
Chat app in C# and ~~MS SQL~~ postgreSQL. Supports user accounts and direct messages.

![image](https://user-images.githubusercontent.com/27998891/159732253-5816a9c6-344b-4f36-93ec-fbb523b9f4a4.png)
![image](https://user-images.githubusercontent.com/27998891/159732153-ef745245-5d08-4de1-98c8-849fa5f5df39.png)
![image](https://user-images.githubusercontent.com/27998891/159732343-b388ab75-f4e8-48c2-bb7e-5b7bade91d2b.png)


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
