docker run -it -p5432:5432 --rm -e POSTGRES_PASSWORD="postgres" -v /opt/summary/postgresql/data:/var/lib/postgresql/data postgres
