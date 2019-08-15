#!/usr/bin/env bash
#
# For Xamarin iOS, change the version name located in Info.plist. 
# AN IMPORTANT THING: YOU NEED DECLARE PACKAGE_NAME AND VERSION_NAME ENVIRONMENT VARIABLES IN APP CENTER BUILD CONFIGURATION.

if [ ! -n "$PACKAGE_NAME" ]
then
    echo "You need define the PACKAGE_NAME variable in App Center"
    exit
fi

if [ ! -n "$VERSION_NAME" ]
then
    echo "You need define the VERSION_NAME variable in App Center"
    exit
fi

if [ ! -n "$APP_SECRET_IOS" ]
then
    echo "You need to define the APP_SECRET_IOS variable in App Center"
    exit
fi

INFO_PLIST_FILE=$APPCENTER_SOURCE_DIRECTORY/Chameleon.iOS/Info.plist
VERSION=${VERSION_NAME}.${APPCENTER_BUILD_ID}
SHORT_VERSION=${VERSION_NAME}

if [ ${APPCENTER_BRANCH} != "master" ]
then
    SHORT_VERSION=${SHORT_VERSION}.${APPCENTER_BRANCH}
fi

if [ -e "$INFO_PLIST_FILE" ]
then
    echo "Updating package name to $PACKAGE_NAME in Info.plist"
    plutil -replace CFBundleIdentifier -string $PACKAGE_NAME $INFO_PLIST_FILE

    echo "Updating short version string to $SHORT_VERSION in Info.plist"
    plutil -replace CFBundleShortVersionString -string $SHORT_VERSION $INFO_PLIST_FILE

    echo "Updating version string to ${VERSION}"
    plutil -replace CFBundleVersion -string $VERSION $INFO_PLIST_FILE

    if [ -n "$LAUNCH_ICON" ]
    then
        echo "Updating the App Icon to ${LAUNCH_ICON}"
        plutil -replace XSAppIconAssets -string ${LAUNCH_ICON} $INFO_PLIST_FILE
    fi

    echo "File content:"
    cat $INFO_PLIST_FILE
fi

SETTINGS_FILE=$APPCENTER_SOURCE_DIRECTORY/Chameleon.Services/AppSettings.cs
if [ -e "$SETTINGS_FILE" ]
then

	echo "Arguments for updating:"
	echo " - AppSecret: $APP_SECRET_IOS"

	# Updating ids

	sed -i '' "s/APP_SECRET_ANDROID/$APP_SECRET_ANDROID/g" $SETTINGS_FILE
	sed -i '' "s/APP_SECRET_IOS/$APP_SECRET_IOS/g" $SETTINGS_FILE
	sed -i '' "s/APP_SECRET_UWP/$APP_SECRET_UWP/g" $SETTINGS_FILE

	# Print out file for reference
	cat $SETTINGS_FILE

	echo "Updated id!"
fi
