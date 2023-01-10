-- CREATE SCHEMA userdata;
-- CREATE TABLE userdata.logins (
--     name VARCHAR(200) NOT NULL,
--     password VARCHAR(200) NOT NULL
-- );

CREATE SCHEMA chengeta;
CREATE TABLE chengeta.sounds (
     id integer primary key,
     time timestamp NOT NULL,
     nodeid BIGINT NOT NULL,
     latitude VARCHAR(200) NOT NULL,
     longitude VARCHAR(200) NOT NULL,
     soundtype VARCHAR(200) NOT NULL,
     probability SMALLINT NOT NULL,
     soundfile VARCHAR(500) NOT NULL,
     status VARCHAR(100) NOT NULL
);