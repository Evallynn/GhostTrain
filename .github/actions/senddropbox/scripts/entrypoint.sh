#!/bin/sh -l

# Print working direction and contents for clarity.
pwd
ls -al

# Tell the user what file we're trying to send.
echo "Attempting to send file $1"
    
# Upload our file (150MB limit atm).
curl -X POST https://content.dropboxapi.com/2/files/upload \
    --header "Authorization: Bearer $2" \
    --header "Dropbox-API-Arg: {\"path\": \"/$3/$1\",\"mode\": \"overwrite\",\"autorename\": true,\"mute\": false}" \
    --header "Content-Type: application/octet-stream" \
    --data-binary @$1

# Return the URL of the upload that we sent.
echo "::set-output name=url::testing"