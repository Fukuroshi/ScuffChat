create procedure messageSend
@name varchar(32), @message varchar(2000)
AS
insert into messages values (GETDATE(), @name, @message)