#!/bin/sh

host="$1"
port="$2"

shift 2  # this removes $1 and $2 so "$@" now contains the actual app command

echo "Waiting for $host:$port to be available..."

while ! nc -z "$host" "$port"; do
  sleep 1
done

echo "$host:$port is now available."

exec "$@"  # <- run the actual command like: dotnet ProductService.Api.dll
