FROM mcr.microsoft.com/mssql/server:2017-latest-ubuntu

RUN mkdir -p /opt/scripts
COPY initialize.sql /opt/scripts/

ENV ACCEPT_EULA Y
ENV SA_PASSWORD S3nh@123
ENV MSSQL_PID Express
RUN /opt/mssql/bin/sqlservr & sleep 60; /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'S3nh@123' -d master -i /opt/scripts/initialize.sql