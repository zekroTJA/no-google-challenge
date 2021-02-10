FROM node:15-alpine3.10 AS build
WORKDIR /build
COPY . .
RUN npm ci
RUN npx ng build --prod

FROM nginx:mainline-alpine AS publish
WORKDIR /app
COPY --from=build /build/dist/WebApp .
COPY config/nginx.conf /etc/nginx/conf.d/webapp.conf
EXPOSE 8080
