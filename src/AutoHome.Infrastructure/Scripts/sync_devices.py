import json
import os
import sys

import tinytuya

if len(sys.argv) < 2:
    print("uso: sync_devices.py <caminho_saida>", file=sys.stderr)
    sys.exit(1)

output_path = sys.argv[1]

api_key = os.environ.get("TUYA_API_KEY")
api_secret = os.environ.get("TUYA_API_SECRET")
api_region = os.environ.get("TUYA_API_REGION", "us")

if not api_key or not api_secret:
    print("TUYA_API_KEY e TUYA_API_SECRET nao definidos", file=sys.stderr)
    sys.exit(1)

try:
    cloud = tinytuya.Cloud(
        apiRegion=api_region,
        apiKey=api_key,
        apiSecret=api_secret
    )
    devices = cloud.getdevices()
except Exception as exc:
    print(str(exc), file=sys.stderr)
    sys.exit(1)

if isinstance(devices, dict) and "Error" in devices:
    print(devices.get("Error", "erro desconhecido"), file=sys.stderr)
    sys.exit(1)

os.makedirs(os.path.dirname(os.path.abspath(output_path)), exist_ok=True)

with open(output_path, "w", encoding="utf-8") as handle:
    json.dump(devices, handle, indent=2)

print(json.dumps({"count": len(devices)}))