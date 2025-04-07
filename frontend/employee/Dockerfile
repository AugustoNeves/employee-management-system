FROM node:20-alpine
WORKDIR /app
COPY frontend/employee .
RUN npm install 
RUN npm run build
EXPOSE 4173
CMD ["npm", "run", "dev", "--", "--host", "--strictPort" ]