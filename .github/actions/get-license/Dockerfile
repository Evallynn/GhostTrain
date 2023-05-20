FROM unityci/editor:2022.2.19f1-windows-mono-1.1

LABEL "com.github.actions.name"="unity_2022.2.19f1"
LABEL "com.github.actions.description"="Run Unity_2022.2.19f1 for license activation."
LABEL "com.github.actions.icon"="box"
LABEL "com.github.actions.color"="gray-dark"

LABEL "repository"="http://github.com/GhostTrain"
LABEL "homepage"="http://github.com/GhostTrain"
LABEL "maintainer"="Luke Staddon"

COPY ./scripts/request_activation.sh /request_activation.sh
RUN chmod +x /request_activation.sh

ENTRYPOINT ["/request_activation.sh"]