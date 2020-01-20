import json

import cv2
import numpy as np

img = cv2.imread('data/1341000-0_UCMR_6000011a1_1562654575151.jpg', cv2.IMREAD_UNCHANGED)

with open("data/1341000-0_UCMR_6000011a1_1562654575151.txt", "r") as file:
    data = file.read()

parsed_json = json.loads(data)

# print(len(parsed_json["Lines"]))

# print(img.shape)

for line in parsed_json["Lines"]:
    for word in line["Words"]:
        # print(f'{word["BoundingRect"]["X"]} {word["BoundingRect"]["Y"]}')

        top_left_x = int(word["BoundingRect"]["X"])
        top_left_y = int(word["BoundingRect"]["Y"])

        bottom_right_x = top_left_x + int(word["BoundingRect"]["Width"])
        bottom_right_y = top_left_y + int(word["BoundingRect"]["Height"])


        print("---------------")

        print(top_left_x)
        print(top_left_y)
        print(bottom_right_x)
        print(bottom_right_y)


        cv2.rectangle(img, (top_left_x, top_left_y), (bottom_right_x, bottom_right_y), (255, 255, 0), 2)


cv2.imshow("contours", img)

while True:
    key = cv2.waitKey(1)
    if key == 27: #ESC key to break
        break

cv2.destroyAllWindows()