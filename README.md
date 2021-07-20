# ScuffChat
Chat app in C# and MS SQL

# Setup
## SQL Server
### Set the database up to look like this (including the name of the table)
![gay](https://github.com/Fukuroshi/ScuffChat/blob/master/image_2021-06-20_195139.png)

### Add user called **chat**

### Run following queries:
```sql
create procedure messageList
AS
select * from messages
```
```sql
create procedure messageSend
@name varchar(32), @message varchar(2000)
AS
insert into messages values (GETDATE(), @name, @message)
```
```sql
grant execute on messageSend to chat

grant execute on messageList to chat
```

### Connect to IP

# It just works™
