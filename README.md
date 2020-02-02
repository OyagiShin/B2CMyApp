# Name

B2cMyApp

#OverView
Azure AD B2Cテナントと連携確認用のアプリケーションです。
Azure AD B2Cに設定されている、ドメイン名、アプリケーションID、各種ユーザーフロー名をappsettings.jsonに記載していただければ使えます。
また、こちらはしばやんさんのhttps://blog.shibayan.jp/entry/20191008/1570543875に書いてある、
『Microsoft.AspNetCore.Authentication.AzureADB2C.UI 3.1.1』のライブラリの実装をコピーして、動作するようにしたものです。
先ほど言ったテナントのドメイン名や、テナントで設定したアプリケーション名を書き換えるだけで、任意のテナントでアカウント管理ができます。

B2Cテナント側の設定は記事が書けましたら、こちらにもリンクを貼ろうと思います。ぜひ、合わせてごらんください。

# Features


# Requirement
使用しているnuget
・Microsoft.AspNetCore.Authentication.JwtBearer
・Microsoft.AspNetCore.Authentication.OpenIdConnect
(・Microsoft.Extensions.Logging.Debug)
(・Microsft.VisualStudio.Web.CodeGeneration.Design)


# Installation
B2Cテナントを作る、アプリケーション登録、各種ユーザーフロー作成後に、このアプリで連携を行ってください。
設定場所は、appsettings.jsonになります。

# Usage
実行するだけです。

# Note
Azure AD B2Cが必須です。

# Author

* 作成者：やまひつじ
* 所属：個人
* E-mail：ooyagi.shin@gmail.com

