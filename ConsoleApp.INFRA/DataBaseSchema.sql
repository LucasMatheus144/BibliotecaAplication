create sequence cliente_seq;

create table cliente(
	id integer primary key default nextval('cliente_seq'),
	nome varchar(20) not null,
	email varchar(50),
	cpf varchar(14) not null unique,
	d_datanascimento date,
	d_ultimaalteracao date default now()
);

create index idx_cliente_id on cliente(id);

create sequence livros_seq;

create table livros(
	id integer primary key default nextval('livros_seq'),
	nome varchar(30) not null unique,
	descricao varchar(50),
	situaca integer not null
);
create index idx_livro_id on livros(id);


create sequence emprestimo_seq;

create table emprestimo(
	id integer primary key default nextval('emprestimo_seq'),
	id_cliente integer not null,
	id_livro integer not null,
	d_dataretirada date not null,
	d_datadevolucao date not null,
	devolvido boolean default false,
	FOREIGN KEY (id_cliente) references cliente(id),
	FOREIGN KEY (id_livro)   references livros(id)
);

create index idx_emprestimo_id on emprestimo(id);
create index idx_emprestimo_cliente_id on emprestimo(id_cliente);
create index idx_emprestimo_livro_id on emprestimo(id_livro);

