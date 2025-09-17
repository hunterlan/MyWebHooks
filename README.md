# MyWebHooks

**Author**: Kostiantyn Sharykin

## Description

This project was created to demonstrate skills in developing web hooks as both sides: publishing and subscriptions.

## How to launch it?

The order of launch is important. Sender project should be launched first, then receiver should be launched. It's
required because if receiver can't subscribe to webhook of sender, it won't launch.

Also, you need to set up Serilog configuration and other important values in `appsetting.json`, here's the next example:

```json
{
  "Serilog": {
    "Using": ["Serilog.Sinks.Async"],
    "MinimumLevel": "Warning",
    "WriteTo": [
      {
        "Name": "Async",
        "Args": {
          "path": "log.txt",
          "shared": true,
          "outputTemplate": "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}"
        }
      }
    ]
  }
}
```

If you want to test it, you need to send to `Sender` a POST request to `items` resource. After that, `Sender` will
notify all registered subscribers.
