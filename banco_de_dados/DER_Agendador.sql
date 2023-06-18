CREATE TABLE endereco (
   codigo_end INTEGER NOT NULL IDENTITY(1,1) PRIMARY KEY,   
   logradouro varchar(100),
   bairro varchar(50),
   numero integer,
   cep varchar(8),
);

create table empresa (
   codigo_emp INTEGER NOT NULL IDENTITY(1,1) PRIMARY KEY, 
   codigo_end integer references endereco(codigo_end),
   cnpj varchar(14),
   nome varchar(50),
   telefone varchar(15),   
   registro_ativo bit default 1
);

CREATE TABLE servico(
  codigo_srv INTEGER NOT NULL IDENTITY(1,1) PRIMARY KEY,
  codigo_emp integer references empresa(codigo_emp),
  descricao varchar(100),
  valor numeric(14,2),
  registro_ativo bit default 1
);

create table profissional (
   codigo_pro INTEGER NOT NULL IDENTITY(1,1) PRIMARY KEY,
   codigo_emp integer references empresa(codigo_emp),
   nome varchar(50),
   atividade varchar(100),
   registro_ativo bit default 1
)

CREATE TABLE servico_profissional(
  codigo_servprof INTEGER NOT NULL IDENTITY(1,1) PRIMARY KEY,
  codigo_pro INTEGER REFERENCES profissional(codigo_pro),
  codigo_srv INTEGER references servico(codigo_srv),
  registro_ativo bit default 1
);


CREATE TABLE horario(
  codigo_hor INTEGER NOT NULL IDENTITY(1,1) PRIMARY KEY,
  codigo_servprof INTEGER REFERENCES servico_profissional(codigo_servprof),
  dia_semana integer, 
  inicio time,
  fim time,
  registro_ativo bit default 1
);

create table cliente (
   codigo_cli INTEGER NOT NULL IDENTITY(1,1) PRIMARY KEY,
   nome varchar(80),
   telefone varchar(15),
   registro_ativo bit default 1
);

CREATE TABLE agendamento(
  codigo_age INTEGER NOT NULL IDENTITY(1,1) PRIMARY KEY,
  codigo_cli INTEGER REFERENCES cliente(codigo_cli),
  codigo_hor INTEGER references horario(codigo_hor),
  data date,
  registro_ativo bit default 1
);

insert into endereco (logradouro, bairro, numero, cep) values ('Rua das Palmeiras', 'Centro', 480, '85907000');
insert into empresa (codigo_end, cnpj, nome, telefone) values (1, '47358142000199', 'Jonas e CIA', '4535657898')
insert into servico (codigo_emp, descricao, valor) values (1, 'Corte de cabelo Masculino', 30);
insert into servico (codigo_emp, descricao, valor) values (1, 'Corte de cabelo Feminino', 35);
insert into servico (codigo_emp, descricao, valor) values (1, 'Hidratação', 20);
insert into servico (codigo_emp, descricao, valor) values (1, 'Escova', 15);
insert into profissional(codigo_emp, nome, atividade) values (1, 'José Carlos de Lima', 'Cabeleireiro');
insert into servico_profissional(codigo_pro, codigo_srv) values (1, 1);
insert into servico_profissional(codigo_pro, codigo_srv) values (1, 2);
insert into servico_profissional(codigo_pro, codigo_srv) values (1, 4);
insert into horario(codigo_servprof, inicio, fim) values (1, '08:00', '09:00');
insert into horario(codigo_servprof, inicio, fim) values (1, '09:00', '10:00');
