CREATE SMALLFILE TABLESPACE USERS_TABLESPACE DATAFILE 'C:\Users\Danilo\Oracle\Tablespace\USERS1.dbf' SIZE 30M;

ALTER SESSION SET "_ORACLE_SCRIPT"=TRUE;

CREATE USER CADDEVDB IDENTIFIED BY CADDEVDB
    DEFAULT TABLESPACE USERS_TABLESPACE
    QUOTA UNLIMITED ON USERS_TABLESPACE;
    
GRANT CREATE TABLE, CREATE SESSION, CREATE DATABASE LINK, CREATE MATERIALIZED VIEW, CREATE PROCEDURE,
CREATE PUBLIC SYNONYM, CREATE ROLE, CREATE SEQUENCE, CREATE SYNONYM, CREATE TRIGGER, CREATE TYPE,
 CREATE VIEW TO CADDEVDB;