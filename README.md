 ![GitHub](https://img.shields.io/github/license/shells-dw/loupedeck-pihole)
 ![GitHub last commit](https://img.shields.io/github/last-commit/shells-dw/loupedeck-pihole)
  ![GitHub downloads](https://img.shields.io/github/downloads/shells-dw/loupedeck-pihole/total)
 [![Tip](https://img.shields.io/badge/Donate-PayPal-green.svg)]( https://www.paypal.com/donate?hosted_button_id=8KXD334CCEEC2) / [![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/Y8Y4CE9LH)


# Unofficial Loupedeck IFTTT Webhook Plugin


## What Is This (and what does it do?)

It's a plugin for the [Loupedeck Live Consoles][Loupedeck] that can trigger [IFTTT][IFTTT] webhooks with values and json payload.

**Works on Windows and MacOS**


## Release / Installation

You can find the precompiled plugin lplug4 file in the [Releases][Releases]. Download and open it, your computer should already recognize this as a Loupedeck plugin file and offer to open it with Loupedeck Configuration Console - which will have the plugin available in the list then.

## Prerequisites 

You need an IFTTT account (free or Pro), connect it to Webhooks on the [IFTTT Webhooks service site](https://ifttt.com/maker_webhooks) and have Webhook Applets set up to trigger with your Loupedeck.

## Setup

 - Visit the [IFTTT Webhooks service site](https://ifttt.com/maker_webhooks) and click on "Documentation" to retrieve your individual access token/key. It's presented on the very top of the page, behind "Your key is:" :wink:
 - Enter (paste) that key in the events.json config file of the plugin (see [next chapter](#events.json)).
 - Add the name of your event and what values or json payload you want to send as per the example provided in the file.
 - Save the file, select the event in the drop-down list and put the button on the Loupedeck.

## events.json
**Windows**: %localappdata%\Loupedeck\PluginData\Ifttt

**MacOS**: /Users/USERNAME/.local/share/Loupedeck/PluginData/Ifttt

contains the file events.json

```json
{
  "iftttKey": "",
  "events": {
    "Example 1": [
      {
        "value1": "value1",
        "value2": "value2",
        "value3": "value3"
      }
    ],
    "Example 2": [
      { "this": [ { "is": { "some": [ "test", "data" ] } } ] }
    ]
  }
}
```
Add your webhook key to the value `iftttKey`.

Events are to be added (or the examples overwritten) inside `events`.

The example event `Example 1` is to trigger an event with up to 3 values. IFTTT allows up to 3, but doesn't require them. You can remove all "value" fields or just use 1 or 2.

The example `Example 2` shows how to send an event with arbitrary json payload.


## Usage

After editing the events.json config file removing/overwriting example events and adding your own, select the Event you want to send in the dropown of the plugin action "IFTTT webhooks", save, pull it on the Loupedeck and the button will now trigger the event you've set.

_Note: if your events don't show in the dropdown after editing, restart the Loupedeck service_

# I have an issue or miss a feature

You can submit an issue or request a feature with [GitHub issues]. Please describe as good as possible what went wrong or doesn't act like you'd expect it to. 

# Support

If you'd like to drop me a coffee for the hours I've spent on this:
[![Tip](https://img.shields.io/badge/Donate-PayPal-green.svg)]( https://www.paypal.com/donate?hosted_button_id=8KXD334CCEEC2)
or use Ko-Fi [![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/Y8Y4CE9LH)


# Changelog
## [1.0.1] - 2022-12-29
- Added logging
- Clarified plugin name shown in UI, naming in events.json and README.MD

<details><summary>Changelog History</summary><p>

## [1.0.0] - 2022-12-09
### Added
Initial release

</p></details>

<!-- Reference Links -->

[Loupedeck]: https://loupedeck.com "Loupedeck.com"
[Releases]: https://github.com/shells-dw/loupedeck-ifttt/releases "Releases"
[IFTTT]: https://ifttt.com "IFTTT"
[GitHub issues]: https://github.com/shells-dw/loupedeck-ifttt/issues "GitHub issues link"

