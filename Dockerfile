
# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS prod
WORKDIR /app
# EXPOSE 8080

COPY . .

RUN ["dotnet", "tool", "install", "--global" ,"dotnet-ef"]
ENV DOTNET_TOOLS="/root/.dotnet/tools"
ENV PATH="$PATH:${DOTNET_TOOLS}"
RUN ["sh", "-c", "sleep 20"]
ENTRYPOINT [ "dotnet" ]
CMD ["watch", "run"]









