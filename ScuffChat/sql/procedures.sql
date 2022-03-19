create procedure register(name varchar(32), pass varchar(32))
language "sql"
as $u$
insert into users (username, password, online, lastonline) 
values (name, crypt(pass, gen_salt('md5')), false, current_timestamp(0))
$u$;

CREATE procedure messageSend(name varchar(32), message varchar(3000))
language "sql"
as $u$
insert into messages (timestamp, username, contents)
values (current_timestamp(0), name, message)
$u$;

CREATE procedure sendDM(a varchar(32), b varchar(32), message varchar(3000))
language "sql"
as $u$
insert into dms (timestamp, sender, recipient, contents)
values (current_timestamp(0), a, b, message)
$u$;

CREATE procedure offline(name varchar(32))
language "sql"
as $u$
update users
set online = false, lastonline = current_timestamp(0)
where username = name
$u$;

CREATE procedure online(name varchar(32))
language "sql"
as $u$
update users
set online = true
where username = name
$u$;