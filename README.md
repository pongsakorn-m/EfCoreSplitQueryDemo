# EF Core Split Query Demo

## Overview

This is a simple **.NET 8 console application** demonstrating the difference between EF Core's:

- **Include** (single SQL query with joins)
- **AsSplitQuery** (multiple SQL queries to avoid row duplication)

The app uses **Entity Framework Core** with **SQL Server** running in Docker.

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)

---

## Setup and Run

### 1. Start SQL Server container

```bash
docker-compose up -d
