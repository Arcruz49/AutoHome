import json
import sys
import tinytuya

try:
    devices = tinytuya.deviceScan(verbose=False, poll=False)
except Exception as exc:
    print(str(exc), file=sys.stderr)
    sys.exit(1)

result = []
for ip, info in devices.items():
    result.append({
        "deviceId": info.get("gwId", ""),
        "ipAddress": ip,
        "productId": info.get("productKey", ""),
        "version": str(info.get("version", ""))
    })

print(json.dumps(result))