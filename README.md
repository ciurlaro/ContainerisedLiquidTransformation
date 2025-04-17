# Liquid Transformation – Containerized Web UI

A modern, container-native version of the LiquidTransformation project with a fully integrated web interface. No .NET SDK installation required.

---

## 🚀 Features

- 🖥  Web-based UI with drag-and-drop upload
- 🌈 Output rendering with syntax highlighting for:
  - JSON (with collapsible keys)
  - CSV (styled table)
  - XML & HTML (highlighted markup)
  - YAML
- 📦 Single Docker image, multi-arch (amd64/arm64)
- 🐳 Fully containerized – no local SDK needed
- 🔐 Runs as non-root for security

---

## 🧰 Prerequisites

- Docker (or Docker Desktop)
- `docker buildx` and `docker compose` enabled

---

## 🧪 Run locally

```bash
git clone https://github.com/<your-fork>/ContainerisedLiquidTransformation.git
cd ContainerisedLiquidTransformation

docker buildx bake        # build once for current platform

docker compose up         # run the app locally
```

Then open: [http://localhost:8080](http://localhost:8080)

---

## 🛠 Project Structure

```text
ContainerisedLiquidTransformation/
├── Code/
│   ├── LiquidTransform/             # original CLI
│   ├── LiquidTransformationLib/     # core logic
│   ├── LiquidTransform.Web/         # ASP.NET Core API + static UI
│   │   ├── wwwroot/index.html       # interactive web interface
│   └── Samples/                     # sample input files
├── Dockerfile                       # multi-stage, portable build
├── docker-compose.yml               # local dev entrypoint
├── docker-bake.hcl                  # portable cross-platform build config
└── README.md
```

---

## 📦 Docker Buildx (multi-arch or native)

```bash
docker buildx bake                # builds for your current platform
# or:
docker buildx bake --push         # builds & pushes to registry (optional)
```

---

## 📤 API endpoint

```bash
curl -F template=@cars.liquid \
     -F content=@cars.json \
     http://localhost:8080/api/transform
```

---

## 🌐 Access the Web UI

Once the container is running, open your browser at:

http://localhost:8080


This will serve the `index.html` file from `Code/LiquidTransform.Web/wwwroot/`, giving you access to the drag‑and‑drop interface for transforming JSON using Liquid templates.


---

## 👀 Output rendering support

| Format | Detection Method        | Rendered As                  |
|--------|-------------------------|------------------------------|
| JSON   | `JSON.parse` succeeds   | Highlighted, pretty-printed  |
| CSV    | First line has commas   | Bootstrap table              |
| XML    | `DOMParser` valid XML   | Indented, highlighted markup |
| YAML   | `:` and newline present | Highlighted YAML             |
| HTML   | Starts with `<tag>`     | Highlighted (escaped)        |

---

## 🧼 Security
- All output is escaped or highlighted safely
- App runs as `USER app` (non-root)
- CORS enabled for local dev (`AllowAnyOrigin`)

---

## 🧪 Smoke Test

```bash
# CLI fallback (only if built into container)
docker exec -it $(docker compose ps -q liquid) ./LiquidTransform -h

# API
curl -F template=@Code/Samples/carsff_cs.liquid \
     -F content=@Code/Samples/cars.json \
     http://localhost:8080/api/transform | jq .
```

---

## 📄 License

MIT. Original project by [@skastberg](https://github.com/skastberg). Containerized version + UI by [@ciurlaro](https://github.com/ciurlaro).
