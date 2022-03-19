CREATE OR REPLACE FUNCTION messageList(
	)
    RETURNS TABLE("ID" bigint,
				 "timestamp" timestamp with time zone,
				 username varchar(32),
				 contents varchar(3000))
    LANGUAGE 'plpgsql'

AS $BODY$
begin
return query
select * from messages;
end
$BODY$;

CREATE OR REPLACE FUNCTION dmList(a varchar(32), b varchar(32))
    RETURNS TABLE("ID" bigint,
				 "timestamp" timestamp with time zone,
				 sender varchar(32),
                 recipient varchar(32),
				 contents varchar(3000))
    LANGUAGE 'plpgsql'

AS $BODY$
begin
return query
select * from dms
where dms.sender = a and dms.recipient = b or dms.sender = b and dms.recipient = a;
end
$BODY$;

CREATE OR REPLACE FUNCTION dmAmount(a varchar(32), b varchar(32))
    RETURNS TABLE("count" bigint)
    LANGUAGE 'plpgsql'
AS $BODY$
begin
return query
select count(*) from dms
where sender = a and recipient = b or sender = b and recipient = a;
end
$BODY$;

CREATE OR REPLACE FUNCTION messageAmount()
    RETURNS TABLE("count" bigint)
    LANGUAGE 'plpgsql'
AS $BODY$
begin
return query
select count(*) from messages;
end
$BODY$;

CREATE OR REPLACE FUNCTION isOnline(a varchar(32))
    RETURNS TABLE("onl" boolean,
    "lastonl" timestamp with time zone)
    LANGUAGE 'plpgsql'
AS $BODY$
begin
return query
select online, lastonline from users where username = a;
end
$BODY$;

CREATE OR REPLACE FUNCTION login(a varchar(32), b varchar(32))
    RETURNS TABLE("user" varchar(32))
    LANGUAGE 'plpgsql'
AS $BODY$
begin
return query
select username from users
where username = user and password=crypt(b, password);
end
$BODY$;

CREATE OR REPLACE FUNCTION offlineUsers()
    RETURNS TABLE("user" varchar(32))
    LANGUAGE 'plpgsql'
AS $BODY$
begin
return query
select username from users
where online=false;
end
$BODY$;

CREATE OR REPLACE FUNCTION onlineUsers()
    RETURNS TABLE("user" varchar(32))
    LANGUAGE 'plpgsql'
AS $BODY$
begin
return query
select username from users
where online=true;
end
$BODY$;

CREATE OR REPLACE FUNCTION onlineUserAmount()
    RETURNS TABLE("count" int)
    LANGUAGE 'plpgsql'
AS $BODY$
begin
return query
select count(username) from users
where online=true;
end
$BODY$;
