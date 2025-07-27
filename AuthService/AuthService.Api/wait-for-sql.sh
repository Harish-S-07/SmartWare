#!/bin/bash

host="$1"
port="$2"
shift 2
cmd="$@"

echo "Waiting for SQL Server at $host:$port..."

while ! nc -z "$host" "$port"; do
  echo "SQL Server not available yet - sleeping"
  sleep 3
done

echo "SQL Server is up - executing command"
exec $cmd
