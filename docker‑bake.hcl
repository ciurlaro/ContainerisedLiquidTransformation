group "default" { targets = ["liquid"] }

target "liquid" {
  context    = "."
  dockerfile = "Dockerfile"
  tags       = ["ghcr.io/ciurlaro/liquidtransform:latest"]
}
