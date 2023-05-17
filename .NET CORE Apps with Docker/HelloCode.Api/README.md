

## .vscode/lunch.json
After adding the docker support with vs-code, It will populate the lunch json file with below.
```
    {
            "name": "Docker .NET Launch",
            "type": "docker",
            "request": "launch",
            "preLaunchTask": "docker-run: debug",
            "netCore": {
                "appProject": "${workspaceFolder}/HelloCode.Api.csproj"
            },
            "dockerServerReadyAction": {
                "uriFormat": "%s://localhost:%s/swagger"
            }
    }
```


Add Below lunch browser option to lunch browser.
```
 "launchBrowser": {
                "enabled": true,
                "args": "${auto-detect-url}/swagger",
                "windows": {
                    "command": "cmd.exe",
                    "args": "/C start ${auto-detect-url}/swagger"
                }
            },
```

## .vscode/Task.json
will populate with `docker-build`, `docker-run` for buth debug and release.
```
{
            "type": "docker-build",
            "label": "docker-build: debug",
            "dependsOn": [
                "build"
            ],
            "dockerBuild": {
                "tag": "hellocodeapi:dev",
                "target": "base",
                "dockerfile": "${workspaceFolder}/Dockerfile",
                "context": "${workspaceFolder}",
                "pull": true
            },
            "netCore": {
                "appProject": "${workspaceFolder}/HelloCode.Api.csproj"
            }
        },
        {
            "type": "docker-build",
            "label": "docker-build: release",
            "dependsOn": [
                "build"
            ],
            "dockerBuild": {
                "tag": "hellocodeapi:latest",
                "dockerfile": "${workspaceFolder}/Dockerfile",
                "context": "${workspaceFolder}",
                "platform": "linux/amd64",
                "pull": true
            },
            "netCore": {
                "appProject": "${workspaceFolder}/HelloCode.Api.csproj"
            }
        },
        {
            "type": "docker-run",
            "label": "docker-run: debug",
            "dependsOn": [
                "docker-build: debug"
            ],
            "dockerRun": {},
            "netCore": {
                "appProject": "${workspaceFolder}/HelloCode.Api.csproj",
                "enableDebugging": true
            }
        },
        {
            "type": "docker-run",
            "label": "docker-run: release",
            "dependsOn": [
                "docker-build: release"
            ],
            "dockerRun": {},
            "netCore": {
                "appProject": "${workspaceFolder}/HelloCode.Api.csproj"
            }
        }
```

## Note
***docker desktop will not show logs when we run the container from the vs-code lunch options**