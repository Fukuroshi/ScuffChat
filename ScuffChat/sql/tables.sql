CREATE TABLE public.messages
(
    "ID" bigint NOT NULL GENERATED ALWAYS AS IDENTITY,
    "timestamp" timestamp(0) with time zone,
    username varchar(32),
    contents varchar(3000),
    PRIMARY KEY ("ID")
);
CREATE TABLE public.dms
(
    "ID" bigint NOT NULL GENERATED ALWAYS AS IDENTITY,
    "timestamp" timestamp(0) with time zone,
    sender varchar(32),
    recipient varchar(32),
    contents varchar(3000),
    PRIMARY KEY ("ID")
);
CREATE TABLE public.users
(
    "ID" bigint NOT NULL GENERATED ALWAYS AS IDENTITY,
    username varchar(32) NOT NULL,
    password varchar(300),
    online boolean,
    lastonline timestamp(0) with time zone,
    PRIMARY KEY ("ID")
);
CREATE EXTENSION IF NOT EXISTS pgcrypto;

CREATE ROLE chat WITH
	LOGIN
	NOSUPERUSER
	NOCREATEDB
	NOCREATEROLE
	NOINHERIT
	NOREPLICATION
	CONNECTION LIMIT -1
	PASSWORD '12341234';

