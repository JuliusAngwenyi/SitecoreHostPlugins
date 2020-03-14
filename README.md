# Sitecore Host Plugins
A repository for Sitecore Host Plugins

## Introduction
For background information about what is a Sitecore Host and Host Plugins, refer to [Sitecore Host Part one](https://360agileweb.wordpress.com/2020/02/26/sitecore-host-part-one/) blog post.

In this repository, we have the following Sitecore Host Plugins for extending [Sitecore Identity Server](https://360agileweb.wordpress.com/2020/02/28/sitecore-host-part-three/)
1. Avanade.Plugin.IdentityProvider.Ids4Demo
2. Avanade.Plugin.IdentityProvider.Ids4Adfs
3. Avanade.Plugin.IdentityProvider.Ids4WsFederation
4. Avanade.Plugin.IdentityProvider.Ids4Facebook

### 1. Avanade.Plugin.IdentityProvider.Ids4Demo
This plugin will extend Sitecore Identity Server to with [IdentityServer4 Demo](https://demo.identityserver.io/) provider

### 2. Avanade.Plugin.IdentityProvider.Ids4Adfs
This plugin will extend Sitecore Identity Server with Active Directory Open ID Connect provider

### 3. Avanade.Plugin.IdentityProvider.Ids4WsFederation
This plugin will extend Sitecore Identity Server with Active Directory Federation Services provider

### 4. Avanade.Plugin.IdentityProvider.Ids4Facebook
This plugin will extend Sitecore Identity Server with external Facebook external login

## Installation
You need to Add the Sitecore Identity NuGet Feed to Visual Studio to Build this Project

Tools -> Package Manager Settings -> Package Sources
Add the [Following Feed](https://sitecore.myget.org/F/sc-identity/api/v3/index.json)

## Deploying Host Plug Manually

Plugins are distributed as Nuget packages. To add a plugin to a host application so that it is loaded at runtime, the plugin must be unpacked and have its assets copied to the correct locations.

### 1. Create an environment folder
If you do not have one already, you need to create an environment folder under the&nbsp;<code>sitecoreruntime</code>&nbsp;folder. A&nbsp;Sitecore Host&nbsp;application will default its environment to&nbsp;Production. Unless a different environment is supplied at startup (via the&nbsp;--env&nbsp;command) it will look for the&nbsp;production&nbsp;folder first:

For example:&nbsp; <code>hostapp/sitecoreruntime/production</code>

### 2. Create a plugin folder

You need to create a folder for the plugin (in our case name it <em>Avanade.Plugin.IdentityProvider.Ids4WsFederation</em>) . This is where the plugin manifest, assets and configuration are located. This is located inside the&nbsp;<code>sitecore</code>&nbsp;folder, which is inside an individual environment folder

The&nbsp;<code>sitecoreruntime/&lt;env&gt;/sitecore</code>&nbsp;folder does not override files in the hosts&nbsp;<code>sitecore</code>&nbsp;folder. This is a unique folder used for loading plugin assets.

Runtime environment folders cannot contain a&nbsp;<code>sitecoreruntime</code>&nbsp;folder of their own.

### 3. Unpack plugin data from a Nuget package and deploy it

We have our plugin named <em>Avanade.Plugin.IdentityProvider.Ids4WsFederation.1.0.0.nupkg</em>

Unpack the plugin contents. You will notice our package contains  special&nbsp;<code>sitecore</code>&nbsp;directory in the root of the&nbsp;<code>nupkg</code>&nbsp;package with additional things inside it. Everything else is the standard Nuget structure.

Copy the contents of the Nuget&nbsp;<code>sitecore</code>&nbsp;folder to the plugin folder you created previously (for example,&nbsp;<code> sitecoreruntime/production/sitecore/<em>Avanade.Plugin.IdentityProvider.Ids4WsFederation</em></code>).

Our plugin package contains a&nbsp;<em>lib&nbsp;</em>folder, copy the assets from the correct&nbsp;<a rel="noreferrer noopener" href="https://docs.microsoft.com/en-us/nuget/create-packages/supporting-multiple-target-frameworks#framework-version-folder-structure" target="_blank">target framework</a>&nbsp;to the root of the&nbsp;<code>sitecoreruntime/&lt;env&gt;</code>&nbsp;folder (for example,&nbsp;<code>sitecoreruntime/production/*.dll</code>)

Our plugin package contains a&nbsp;<em>content </em>folder, copy the assets from this folder to the plugin folder created previously (for example,&nbsp;<code> sitecoreruntime/production/sitecore/<em>Avanade.Plugin.IdentityProvider.Ids4WsFederation</em></code>)

The final folder structure will looks similar to this below

<figure class="wp-block-image size-large"><img src="https://360agileweb.files.wordpress.com/2020/03/11.-ws-federation-plugin-deployeddirstructure.png?w=766" alt="" class="wp-image-635"/></figure>


## Registering Azure AD Client
Follow this steps here to register your application with your [Azure Active Directory tenant](https://docs.microsoft.com/en-gb/azure/active-directory/azuread-dev/v1-protocols-openid-connect-code) You will get the Application/Client ID and Tenant ID required for the Azure AD plugin
