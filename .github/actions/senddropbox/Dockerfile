FROM ubuntu

LABEL "com.github.actions.name"="send-to-dropbox"
LABEL "com.github.actions.description"="Send files to dropbox."
LABEL "com.github.actions.icon"="box"
LABEL "com.github.actions.color"="gray-dark"

LABEL "repository"="http://github.com/GhostTrain"
LABEL "homepage"="http://github.com/GhostTrain"
LABEL "maintainer"="Luke Staddon"

COPY ./scripts/entrypoint.sh /entrypoint.sh

RUN chmod +x /entrypoint.sh
RUN apt-get update
RUN apt-get install curl --assume-yes

ENTRYPOINT ["/entrypoint.sh"]