# Etapa 1: Construção da aplicação React
FROM node:20 AS build

WORKDIR /app

# Copiar os arquivos de dependências
COPY package.json package-lock.json ./

# Instalar dependências
RUN npm install

# Copiar todo o código-fonte para dentro do contêiner
COPY . .

# Construir a aplicação React
RUN npm run build

# Etapa 2: Servir a aplicação com Nginx
FROM nginx:1.23.4

# Remover a configuração padrão do Nginx
RUN rm -rf /etc/nginx/conf.d/default.conf

# Copiar configuração personalizada do Nginx
COPY nginx.conf /etc/nginx/conf.d/

# Copiar os arquivos do build React para o Nginx
COPY --from=build /app/build /usr/share/nginx/html

# Expor a porta 80
EXPOSE 80

# Comando padrão do Nginx
CMD ["nginx", "-g", "daemon off;"]
