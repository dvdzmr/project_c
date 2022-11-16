-- CREATE SCHEMA userdata;
-- CREATE TABLE userdata.logins (
--     name VARCHAR(200) NOT NULL,
--     password VARCHAR(200) NOT NULL
-- );

CREATE SCHEMA chengeta;
CREATE TABLE chengeta.sounds (
    time timestamp NOT NULL,
    nodeid BIGINT NOT NULL,
    latitude double precision NOT NULL,
    longitude double precision NOT NULL,
    soundtype VARCHAR(200) NOT NULL,
    probability SMALLINT NOT NULL,
    soundfile VARCHAR(500) NOT NULL                         
);