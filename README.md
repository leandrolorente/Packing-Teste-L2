# Pedido Packing Service

Este projeto é um sistema de empacotamento de pedidos em caixas, desenvolvido em **C#** usando **.NET Core**. Ele utiliza boas práticas de arquitetura, como **DDD (Domain-Driven Design)**, **Clean Architecture**, e os princípios **SOLID**, para garantir um código limpo e escalável.

## Índice

- [Descrição](#descrição)
- [Funcionalidades](#funcionalidades)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Instalação](#instalação)
- [Uso](#uso)
- [Contribuindo](#contribuindo)
- [Licença](#licença)

## Descrição

O **Pedido Packing Service** é responsável por processar uma lista de pedidos e alocar os produtos em caixas de acordo com suas dimensões. O sistema tenta otimizar o empacotamento verificando se todos os produtos cabem em uma única caixa e, se isso não for possível, empacota os produtos individualmente.

## Funcionalidades

- Empacotamento de produtos em caixas, levando em consideração diferentes orientações do produto.
- Tentativa de empacotar todos os produtos de um pedido em uma única caixa.
- Lógica de fallback para empacotar produtos individualmente, caso não caibam juntos.
- Geração de respostas detalhadas com informações sobre quais caixas foram usadas para cada pedido.

## Tecnologias Utilizadas

- **C#**
- **.NET Core 8.0+**
- **ASP.NET Core** (para APIs Web)
- **MediatR** (para implementação do padrão Mediator)
- **Domain-Driven Design (DDD)**
- **Clean Architecture**
- **SOLID Principles**
- **JWT**
- **Docker**
- **xunit**

## Instalação

Para rodar o projeto localmente, siga os passos abaixo:

1. **Clone o repositório:**

    ```bash
    git clone https://github.com/seu-usuario/seu-repositorio.git
    ```

2. **Navegue até o diretório do projeto:**

    ```bash
    cd seu-repositorio
    ```

3. **Restaure os pacotes NuGet:**

    ```bash
    dotnet restore
    ```

4. **Compile o projeto:**

    ```bash
    dotnet build
    ```

5. **Execute o projeto:**

    ```bash
    dotnet run
 
    ```
    
6. **Token para Autenticação no swagger:**
   Qualquer token "jwt" seguindo o que foi definido no appsettings.json
 **Token valido**: eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJzdWFpc3N1ZXJhcXVpIiwiaWF0IjoxNzI5MTk3NjUzLCJleHAiOjE3NjA3MzM2NTMsImF1ZCI6InNldWF1ZGllbmNlYXF1aSIsInN1YiI6IiJ9.4Gvl3Usl2a82ZkprGSHcQ1CJbdqUt6DMWPcO95WsLfQ

## Uso

Uma vez que o projeto esteja rodando, deve usar o token para autenticação e então você pode acessar as APIs expostas para empacotamento de pedidos. Aqui está um exemplo básico de como enviar um pedido para empacotamento:

### Exemplo de Requisição:

```json
POST /api/pedidos/packing
{
  "pedidos": [
    {
      "pedido_id": "123",
      "produtos": [
        { "produto_id": "PS5", "dimensoes": { "altura": 40, "largura": 10, "comprimento": 25 } },
        { "produto_id": "Volante", "dimensoes": { "altura": 40, "largura": 30, "comprimento": 30 } }
      ]
    }
  ]
}
