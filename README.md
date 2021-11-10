# WebApp_AccessDenied

Simple app for problem demonstration.

What app does:
On startup app will create new workspace and copy Plugin.dll in path pointed in `PluginDirectory` setting - from embedded resource, 
then it will remove all old plugins from path defined in `PluginDirectory`.
 
AccessDenied repro steps:
1. Create new App Service
2. Configure App:
```
{
    "name": "ASPNETCORE_ENVIRONMENT",
    "value": "Development",
    "slotSetting": false
  },
  {
    "name": "PluginDirectory",
    "value": "C:\local\temp\plugins",
    "slotSetting": false
  },
  {
    "name": "WEBSITE_DISABLE_OVERLAPPED_RECYCLING",
    "value": "1",
    "slotSetting": false
  },
```
2. Deploy to app service
3. Navigate to `<APP_URL>/api/plugin`
4. AccessDeniedException!
![image](https://user-images.githubusercontent.com/75312900/140955981-94ab53b6-1d1b-4dd7-a7b9-a2af722fff10.png)
5. If application didn't crash, please redeploy app.
