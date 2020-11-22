-------------------- PROCEDURES

SET SERVEROUTPUT ON;

----- 1

SELECT * FROM "Desenvolvedores";

CREATE OR REPLACE PROCEDURE DevDeInst(inst IN "Empresas"."EmpresaID"%TYPE)
AS 
c_devinst SYS_REFCURSOR;
BEGIN 
    OPEN c_devinst FOR 'SELECT "DesenvolvedorID", "Nome", "Email", "Telefone", "EmpresaID"
    FROM "Desenvolvedores" WHERE "EmpresaID" = ' || inst;
    DBMS_SQL.RETURN_RESULT(c_devinst);
END;
/

EXECUTE DevDeInst(1);

select * from "Empresas";

----- 2

CREATE OR REPLACE PROCEDURE LingDeDev(devid IN "Desenvolvedores"."DesenvolvedorID"%TYPE, c_lingdev OUT SYS_REFCURSOR)
IS
BEGIN
    OPEN c_lingdev FOR 'SELECT L."LinguagemID", L."NomeLinguagem"
    FROM "Linguagens" L INNER JOIN "DesenvolvedorLinguagens" DL
    ON L."LinguagemID" = DL."LinguagemID"
    WHERE DL."DesenvolvedorID" = ' || devid;
END;
/

VARIABLE cursor_output refcursor;
EXECUTE LingDeDev(1, :cursor_output);

-------------------- FUNCTIONS

----- 1

CREATE OR REPLACE FUNCTION LingEdit(lingid IN "Linguagens"."LinguagemID"%TYPE, lingname IN "Linguagens"."NomeLinguagem"%TYPE)
RETURN VARCHAR2
IS
mensagem VARCHAR2(30);
BEGIN
    UPDATE "Linguagens"
    SET "NomeLinguagem" = lingname
    WHERE "LinguagemID" = lingid;
    mensagem := 'Editado com sucesso.';
    RETURN mensagem;
END;
/

DECLARE
   v_retorno VARCHAR2(30);
BEGIN
   --EXECUTE IMMEDIATE 'CALL LingEdit(1, PHP) INTO :v_retorno'
   --USING OUT v_retorno;
   v_retorno := LingEdit(3, 'C#');
   DBMS_OUTPUT.put_line('Retorno: '|| v_retorno);
END;
/


SELECT * FROM "Linguagens";

----- 2

CREATE OR REPLACE FUNCTION LingDelete(lingid IN OUT LONG)
RETURN VARCHAR2
IS
mensagem VARCHAR2(30);
BEGIN
    SELECT "NomeLinguagem" INTO mensagem
    FROM "Linguagens" WHERE "LinguagemID" = lingid;
    SELECT "LinguagemID" INTO lingid
    FROM "Linguagens" WHERE "LinguagemID" = lingid;
    DELETE FROM "Linguagens" WHERE "LinguagemID" = lingid;
    RETURN mensagem;
END;
/

DECLARE
   v_call LONG := 3;
   v_retorno VARCHAR2(30);
BEGIN
   v_retorno := LingDelete(v_call);
   DBMS_OUTPUT.put_line('Linguagem: '|| v_retorno || ' deletada');
END;
/

SELECT * FROM "Linguagens";

-------------------- TRIGGERS

----- 1

CREATE OR REPLACE TRIGGER UpdateMsg
AFTER UPDATE
ON "Linguagens"
FOR EACH ROW
ENABLE
DECLARE
    PRAGMA AUTONOMOUS_TRANSACTION;
    TYPE lingEditReg IS RECORD(msg VARCHAR2(50));
    TYPE lingEditArray IS TABLE OF lingEditReg       
    INDEX BY PLS_INTEGER;
    v_lingEditArray lingEditArray;
BEGIN
    v_lingEditArray.Delete();
    v_lingEditArray(1).msg := 'Edição completa';
    DBMS_OUTPUT.PUT_LINE(v_lingEditArray(1).msg);
END;
/


----- 2

CREATE OR REPLACE TRIGGER DeleteMsg
AFTER DELETE
ON "Linguagens"
FOR EACH ROW
ENABLE
DECLARE
    PRAGMA AUTONOMOUS_TRANSACTION;
    TYPE lingDeleteReg IS RECORD(msg VARCHAR2(50));
    TYPE lingDeleteArray IS TABLE OF lingDeleteReg       
    INDEX BY PLS_INTEGER;
    v_lingDeleteArray lingDeleteArray;
BEGIN
    v_lingDeleteArray.Delete();
    v_lingDeleteArray(1).msg := 'Exclusão completa';
    DBMS_OUTPUT.PUT_LINE(v_lingDeleteArray(1).msg);
END;
/