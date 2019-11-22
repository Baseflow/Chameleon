#!/usr/bin/env bash
#
# For Xamarin Android, change the version name located in AndroidManifest.xml. 
# AN IMPORTANT THING: YOU NEED DECLARE PACKAGE_NAME AND VERSION_NAME ENVIRONMENT VARIABLES IN APP CENTER BUILD CONFIGURATION.

if [ ! -n "$PACKAGE_NAME" ]
then
    echo "You need to define the PACKAGE_NAME variable in App Center"
    exit
fi

if [ ! -n "$VERSION_NAME" ]
then
    echo "You need to define the VERSION_NAME variable in App Center"
    exit
fi

if [ ! -n "$APP_SECRET_ANDROID" ]
then
    echo "You need to define the APP_SECRET_ANDROID variable in App Center"
    exit
fi

ANDROID_MANIFEST_FILE=$APPCENTER_SOURCE_DIRECTORY/Chameleon.Droid/Properties/AndroidManifest.xml
VERSION=${VERSION_NAME}.${APPCENTER_BUILD_ID}
if [ ${APPCENTER_BRANCH} != "master" ]
then
    VERSION=${VERSION}.${APPCENTER_BRANCH}
fi

if [ -e "$ANDROID_MANIFEST_FILE" ]
then
    echo "Updating package name to $PACKAGE_NAME in AndroidManifest.xml"
    sed -i '' 's/package="[a-z.]*"/package="'$PACKAGE_NAME'"/' $ANDROID_MANIFEST_FILE

    echo "Updating version name to $VERSION in AndroidManifest.xml"
    sed -i '' 's/versionName="[0-9.]*"/versionName="'${VERSION}'"/' $ANDROID_MANIFEST_FILE

    echo "Updating version code to $APPCENTER_BUILD_ID in AndroidManifest.xml"
    sed -i '' 's/versionCode="[0-9.]*"/versionCode="'${APPCENTER_BUILD_ID}'"/' $ANDROID_MANIFEST_FILE

    # Update the App Icon if an override is specified as environment variable
    if [ -n "$LAUNCH_ICON" ]
    then
        echo "Updating the App Launch icon to ${LAUNCH_ICON}"
        sed -i '' 's/ic_launcher/'${LAUNCH_ICON}'/' $ANDROID_MANIFEST_FILE
    fi

    echo "File content:"
    cat $ANDROID_MANIFEST_FILE
fi

SETTINGS_FILE=$APPCENTER_SOURCE_DIRECTORY/Chameleon.Services/AppSettings.cs
if [ -e "$SETTINGS_FILE" ]
then

	echo "Arguments for updating:"
	echo " - AppSecret: $APP_SECRET_ANDROID"

	# Updating ids

	sed -i '' "s/APP_SECRET_ANDROID/$APP_SECRET_ANDROID/g" $SETTINGS_FILE
	sed -i '' "s/APP_SECRET_IOS/$APP_SECRET_IOS/g" $SETTINGS_FILE
	sed -i '' "s/APP_SECRET_UWP/$APP_SECRET_UWP/g" $SETTINGS_FILE

	# Print out file for reference
	cat $SETTINGS_FILE

	echo "Updated id!"
fi
