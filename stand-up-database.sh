exec docker build -t local/integration-tests-demo.db:latest ./src/Integration-Tests-Demo.Db &
docker run -e ACCEPT_EULA=Y -e SA_PASSWORD=Pa33WorD! -p 1433:1433 local/integration-tests-demo.db:latest