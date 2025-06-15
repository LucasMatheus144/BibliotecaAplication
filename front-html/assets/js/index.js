var URL_API = 'http://localhost:5230';

window.onload = function (){
    listarLivros();
}

// menu lateral
document.addEventListener("DOMContentLoaded", function() {
    const dropdowns = document.querySelectorAll(".menu-dropdown .menu-title");

    dropdowns.forEach(function(dropdown) {
        dropdown.addEventListener("click", function() {
            const parent = dropdown.parentElement;

            if (parent.classList.contains("active")) {
                parent.classList.remove("active");
            } else {
                document.querySelectorAll(".menu-dropdown").forEach(function(item) {
                    item.classList.remove("active");
                });
                parent.classList.add("active");
            }
        });
    });
});

// contrles para form

function AbrirCadastro() {
    const modal = document.getElementById("abrirform");
    const form = document.querySelector("form");

    form.reset()
    modal.classList.remove("close");
}

function FecharCadastro() {
    const modal = document.getElementById("abrirform");
    modal.classList.add("close");
}


// APIS

// listar livros
function listarLivros() {
    fetch(`${URL_API}/api/livro`, {
        method: "GET"
    })
    .then(response => {
        if (response.ok) { 
            return response.json();
        } else {
            console.error('Erro ao buscar livros. Status:', response.status);
        }
    })
    .then(dados => {
        if (!dados) return;  
        const grid = document.querySelector(".grid");

        dados.forEach(livro => {
            const card = document.createElement("div");
            card.classList.add("card");

            // Imagem
            const imgDiv = document.createElement("div");
            const img = document.createElement("img");
            img.src = 'assets/images/percy.png'; 
            img.alt = 'Capa do Livro';
            imgDiv.appendChild(img);

            // Info
            const infoDiv = document.createElement("div");
            infoDiv.classList.add("info");

            // Nome
            const nomeDiv = document.createElement("div");
            nomeDiv.classList.add("nome");

            const labelNome = document.createElement("label");
            labelNome.classList.add("namelivro");
            labelNome.textContent = livro.nome;

            nomeDiv.appendChild(labelNome);

            // Descrição
            const labelDescricao = document.createElement("label");
            labelDescricao.classList.add("descricao");
            labelDescricao.textContent = livro.descricao;

            // Montar
            infoDiv.appendChild(nomeDiv);
            infoDiv.appendChild(labelDescricao);

            card.appendChild(imgDiv);
            card.appendChild(infoDiv);

            grid.appendChild(card);
        });
    })
    .catch(error => console.error('Erro ao chamar a API:', error));
}

async function submitForm(event) {
    event.preventDefault(); // Impede o refresh da página

    const modal = document.getElementById("abrirform");
    const grid = document.querySelector(".grid");
    const form = event.target;
    const oData = new FormData(form);

    const livro = {
        nome: oData.get("namelivro"),
        descricao: oData.get("descricaoLivro"),
        status: 1
    };

    try {
        const response = await fetch(`${URL_API}/api/livro`, {
            method: "POST",
            body: JSON.stringify(livro),
            headers: {
                "Content-Type": "application/json"
            }
        });

        if (response.ok) {
            grid.innerHTML = "";
            listarLivros();
            form.reset();
            modal.classList.add("close");
        } else {
            console.error(`Erro ao salvar: ${response.status}`);
        }
    } catch (error) {
        console.error("Falha na requisição:", error);
    }
}