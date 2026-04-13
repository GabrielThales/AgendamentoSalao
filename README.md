# ✂️ Agendamento Salão de Beleza - API & DevOps Pipeline

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)
![Docker](https://img.shields.io/badge/Docker-Ready-2496ED?logo=docker)
![Terraform](https://img.shields.io/badge/Terraform-AWS-7B42BC?logo=terraform)
![Ansible](https://img.shields.io/badge/Ansible-Configured-EE0000?logo=ansible)
![CI/CD](https://img.shields.io/badge/CI%2FCD-GitHub_Actions-2088FF?logo=github-actions)
![Coverage](https://img.shields.io/badge/SonarCloud-100%25_Coverage-4E9BCD?logo=sonarcloud)

Esta é uma aplicação desenvolvida como projeto prático para a pós-graduação, focada em aplicar as melhores práticas de **Engenharia de Software e DevOps**. 

O projeto consiste em uma API para gestão de agendamentos de um salão de beleza, construída com base em **TDD (Test-Driven Development)** e entregue de forma totalmente automatizada na nuvem (AWS) através de uma esteira de **CI/CD**.

---

## 🎯 O Problema
Salões de beleza frequentemente lidam com conflitos de agenda, marcações em horários inválidos e dificuldade em gerenciar a disponibilidade dos profissionais. Além disso, no aspecto técnico, atualizações manuais de software em servidores causam indisponibilidade do sistema e são propensas a erros humanos.

## 💡 A Solução
Foi desenvolvida uma **API RESTful** que blinda as regras de negócio através de testes unitários rígidos. O sistema impede:
1. Agendamentos em datas e horários que já passaram.
2. Conflito de horários para um mesmo profissional.

Para resolver o problema de infraestrutura, o sistema conta com uma esteira de **Integração e Entrega Contínuas (CI/CD)**. Qualquer nova funcionalidade adicionada ao código passa por testes de qualidade, é empacotada em um contêiner e implantada automaticamente em um servidor na nuvem gerido por Infraestrutura como Código.

---

## 🛠️ Stack Tecnológica

**Desenvolvimento & Qualidade:**
* **C# & .NET 10:** Framework principal utilizando Minimal APIs.
* **NUnit, Moq & Coverlet:** Testes unitários e geração de relatórios de cobertura.
* **SonarCloud:** Análise estática de código e Quality Gate (100% de cobertura alcançada).

**DevOps & Infraestrutura:**
* **Docker:** Containerização da aplicação.
* **GitHub Actions:** Orquestração da esteira de CI/CD.
* **Terraform:** Provisionamento de Infraestrutura como Código (IaC) na AWS (EC2, Security Groups) com controle de estado remoto no S3.
* **Ansible:** Gerenciamento de configuração para instalação do ambiente e deploy do contêiner sem agentes (Agentless).
* **AWS (Amazon Web Services):** Provedor de nuvem para hospedagem.

---

## 🚀 Arquitetura do Pipeline (CI/CD)

A automação do projeto é dividida em 3 grandes etapas no GitHub Actions:

1. **Validação (CI):** Ao realizar um `push` na branch `main`, a esteira compila o código e executa os testes unitários. O relatório é enviado ao SonarCloud. Se a cobertura for menor que 80% ou algum teste falhar, o pipeline é abortado.
2. **Build & Push:** O código é empacotado em uma imagem Docker otimizada (Multi-stage build) e enviada para o repositório público no Docker Hub.
3. **Deploy (CD):** O Terraform valida e provisiona o servidor EC2 na AWS. Em seguida, o Ansible acessa a máquina via SSH, instala o Docker, baixa a imagem atualizada e sobe a aplicação.

---

## ⚙️ Como Executar e Testar o Projeto

### 1. Testando na Nuvem (Ambiente de Produção)
Após a execução com sucesso da esteira no GitHub Actions, a API estará disponível na AWS. Para testar:
1. Vá até a aba **Actions** neste repositório.
2. Clique na última execução bem-sucedida e expanda o Job de **Deploy**.
3. Copie o IP gerado pelo Terraform.
4. Acesse no navegador: `http://[IP_DA_MAQUINA]:8080/swagger`

### 2. Rodando Localmente
Se desejar rodar a aplicação na sua máquina, certifique-se de ter o **.NET 10 SDK** instalado.

```bash
# Clone o repositório
git clone [https://github.com/](https://github.com/)[SEU_USUARIO]/[SEU_REPOSITORIO].git

# Entre na pasta da API
cd AgendamentoSalao/AgendamentoSalao.Api

# Restaure as dependências e rode o projeto
dotnet restore
dotnet run