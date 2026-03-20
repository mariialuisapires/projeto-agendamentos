-- Criação das tabelas
CREATE TABLE sala (
    id   SERIAL PRIMARY KEY,
    nome VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE agendamento (
    id          SERIAL PRIMARY KEY,
    id_sala     INT NOT NULL REFERENCES sala(id),
    data_inicio TIMESTAMP NOT NULL,
    data_fim    TIMESTAMP NOT NULL
);

CREATE TABLE log_operacao (
    id            SERIAL PRIMARY KEY,
    nome_tabela   VARCHAR(50) NOT NULL,
    tipo_operacao VARCHAR(10) NOT NULL,
    data_operacao TIMESTAMP   NOT NULL DEFAULT NOW()
);

-- Função de log
CREATE OR REPLACE FUNCTION fn_registrar_log()
RETURNS TRIGGER AS $$
BEGIN
    INSERT INTO log_operacao (nome_tabela, tipo_operacao, data_operacao)
    VALUES (TG_TABLE_NAME, TG_OP, NOW());
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Triggers de log
CREATE TRIGGER trg_log_sala
AFTER INSERT OR UPDATE OR DELETE ON sala
FOR EACH ROW EXECUTE FUNCTION fn_registrar_log();

CREATE TRIGGER trg_log_agendamento
AFTER INSERT OR UPDATE OR DELETE ON agendamento
FOR EACH ROW EXECUTE FUNCTION fn_registrar_log();

-- Validação de datas
CREATE OR REPLACE FUNCTION fn_validar_agendamento()
RETURNS TRIGGER AS $$
BEGIN
    IF NEW.data_fim <= NEW.data_inicio THEN
        RAISE EXCEPTION 'A data/hora final deve ser maior que a inicial.';
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_validar_agendamento
BEFORE INSERT OR UPDATE ON agendamento
FOR EACH ROW EXECUTE FUNCTION fn_validar_agendamento();

-- Validação de sobreposição
CREATE OR REPLACE FUNCTION fn_verificar_sobreposicao()
RETURNS TRIGGER AS $$
DECLARE
    conflito INT;
BEGIN
    SELECT COUNT(*) INTO conflito
    FROM agendamento
    WHERE id_sala = NEW.id_sala
      AND id != NEW.id
      AND NEW.data_inicio < data_fim
      AND NEW.data_fim > data_inicio;

    IF conflito > 0 THEN
        RAISE EXCEPTION 'Já existe um agendamento para esta sala neste período.';
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_verificar_sobreposicao
BEFORE INSERT OR UPDATE ON agendamento
FOR EACH ROW EXECUTE FUNCTION fn_verificar_sobreposicao();

-- Validação de exclusão de sala
CREATE OR REPLACE FUNCTION fn_validar_exclusao_sala()
RETURNS TRIGGER AS $$
DECLARE
    agendamentos_futuros INT;
BEGIN
    SELECT COUNT(*) INTO agendamentos_futuros
    FROM agendamento
    WHERE id_sala = OLD.id
      AND data_inicio > NOW();

    IF agendamentos_futuros > 0 THEN
        RAISE EXCEPTION 'Não pode ser deletada pois possui agendamentos futuros.';
    END IF;
    RETURN OLD;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_validar_exclusao_sala
BEFORE DELETE ON sala
FOR EACH ROW EXECUTE FUNCTION fn_validar_exclusao_sala();
