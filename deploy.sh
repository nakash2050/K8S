docker build -t nakash2050/employee-spa:latest -t nakash2050/employee-spa:$SHA -f ./SPA/Dockerfile .
docker build -t nakash2050/employee-microservice:latest -t nakash2050/employee-microservice:$SHA -f ./Registration/Dockerfile .
docker build -t nakash2050/employee-cache:latest -t nakash2050/employee-cache:$SHA -f ./CacheService/Dockerfile .

docker push nakash2050/employee-spa:latest
docker push nakash2050/employee-spa:$SHA
docker push nakash2050/employee-microservice
docker push nakash2050/employee-microservice:$SHA
docker push nakash2050/employee-cache
docker push nakash2050/employee-cache:$SHA

kubectl apply -f ./k8s/prod
kubectl set image deployments/client-deployment client=nakash2050/employee-spa:$SHA
kubectl set image deployments/server-deployment server=nakash2050/employee-microservice:$SHA
kubectl set image deployments/cache-deployment cache=nakash2050/employee-cache:$SHA