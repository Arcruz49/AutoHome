import json
import sys

import tinytuya

if len(sys.argv) < 5:
    print("uso: control.py <devices_json> <device_id> <ip> <acao>", file=sys.stderr)
    sys.exit(1)

devices_path, device_id, ip, action = sys.argv[1:5]

DP_SWITCH = "20"
DP_MODE = "21"
DP_BRIGHTNESS = "22"
DP_TEMPERATURE = "23"
DP_COLOUR = "24"

try:
    with open(devices_path, encoding="utf-8") as handle:
        devices = json.load(handle)
except Exception as exc:
    print(f"nao foi possivel ler {devices_path}: {exc}", file=sys.stderr)
    sys.exit(1)

entry = next((d for d in devices if d.get("id") == device_id), None)

if entry is None:
    print(f"device {device_id} nao encontrado em {devices_path}", file=sys.stderr)
    sys.exit(1)

local_key = entry.get("key")

if not local_key:
    print(f"device {device_id} sem local key", file=sys.stderr)
    sys.exit(1)

device = tinytuya.Device(device_id, ip, local_key, version=3.5)
device.set_socketTimeout(5)

try:
    if action == "status":
        result = device.status()
    elif action == "on":
        result = device.set_value(DP_SWITCH, True)
    elif action == "off":
        result = device.set_value(DP_SWITCH, False)
    else:
        print(f"acao desconhecida: {action}", file=sys.stderr)
        sys.exit(1)
except Exception as exc:
    print(str(exc), file=sys.stderr)
    sys.exit(1)

if isinstance(result, dict) and "Error" in result:
    print(f"{result.get('Err', '')} {result.get('Error', '')}".strip(), file=sys.stderr)
    sys.exit(1)

dps = result.get("dps", {}) if isinstance(result, dict) else {}

print(json.dumps({
    "on": dps.get(DP_SWITCH),
    "mode": dps.get(DP_MODE),
    "brightness": dps.get(DP_BRIGHTNESS),
    "temperature": dps.get(DP_TEMPERATURE),
    "colour": dps.get(DP_COLOUR)
}))