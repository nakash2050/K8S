docker build -t nakash2050/employee-spa:latest -t nakash2050/employee-spa:$SHA -f ./SPA/Dockerfile .
docker build -t nakash2050/employee-microservice:latest -t nakash2050/employee-microservice:$SHA -f ./Registration/Dockerfile .

docker push nakash2050/employee-spa:latest
docker push nakash2050/employee-spa:$SHA
docker push nakash2050/employee-microservice
docker push nakash2050/employee-microservice:$SHA

kubectl apply -f k8s-dev
kubectl set image deployments/client-deployment client=nakash2050/employee-spa:$SHA
kubectl set image deployments/server-deployment server=nakash2050/employee-microservice:$SHA