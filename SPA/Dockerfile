### Stage 1: Build ###
FROM node:10-alpine as builder
COPY SPA/package.json SPA/package-lock.json ./
RUN npm install && mkdir /ng-app && mv ./node_modules ./ng-app
WORKDIR /ng-app
COPY ./SPA .
RUN npm run build -- --prod --output-path=dist

### STAGE 2: Setup ###
FROM nginx:1.14.1-alpine
COPY ./SPA/nginx/default.conf /etc/nginx/conf.d/
RUN rm -rf /usr/share/nginx/html/*
COPY --from=builder /ng-app/dist /usr/share/nginx/html
CMD ["/bin/sh",  "-c",  "envsubst < /usr/share/nginx/html/assets/settings.template.json > /usr/share/nginx/html/assets/settings.json && exec nginx -g 'daemon off;'"]