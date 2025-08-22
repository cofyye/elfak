import cv2
import numpy as np
import imutils


def pyramid_resize(image, factor=2.0, min_size=(350, 100)):
    """Generise piramidu slika sa smanjenjem rezolucije."""
    yield image
    while True:
        new_w = int(image.shape[1] / factor)
        image = imutils.resize(image, width=new_w)

        if image.shape[0] < min_size[1] or image.shape[1] < min_size[0]:
            break
        yield image


def crop_region(src_img, wanted_w, wanted_h):
    """Pronalazi region najblizi zadatim dimenzijama."""
    gray_img = cv2.cvtColor(src_img, cv2.COLOR_BGR2GRAY)
    _, thresh = cv2.threshold(gray_img, 127, 255, cv2.THRESH_BINARY)
    contours, _ = cv2.findContours(thresh, cv2.RETR_TREE, cv2.CHAIN_APPROX_SIMPLE)

    chosen_xy = [0, 0]
    best_dx, best_dy = wanted_h, wanted_w

    for cnt in contours:
        if cv2.contourArea(cnt) >= wanted_w * wanted_h:
            perimeter = cv2.arcLength(cnt, True)
            approx = cv2.approxPolyDP(cnt, 0.1 * perimeter, True)

            if len(approx) == 4:
                ys = [pt[0][0] for pt in approx]
                xs = [pt[0][1] for pt in approx]

                min_x, max_x = min(xs), max(xs)
                min_y, max_y = min(ys), max(ys)

                dx = abs(wanted_h - (max_x - min_x))
                dy = abs(wanted_w - (max_y - min_y))

                if dx < best_dx and dy < best_dy:
                    best_dx, best_dy = dx, dy
                    chosen_xy = [min_x, min_y]
                    break

    x, y = chosen_xy
    return src_img[x:x + wanted_h, y:y + wanted_w]


def classify_pets(image, labels, net, step_size=180, win_size=180):
    """Detektuje pse i macke pomocu CNN modela."""
    for resized in pyramid_resize(image):
        for y in range(0, resized.shape[0], step_size):
            for x in range(0, resized.shape[1], step_size):

                patch = resized[y:y + win_size, x:x + win_size]
                if patch.shape[0] < win_size or patch.shape[1] < win_size:
                    continue

                blob = cv2.dnn.blobFromImage(patch, scalefactor=1,
                                             size=(224, 224),
                                             mean=(104, 117, 123))
                net.setInput(blob)
                preds = net.forward()

                idx = np.argmax(preds[0])
                conf = preds[0][idx]

                if conf > 0.5:
                    ratio = int(image.shape[1] / resized.shape[1])
                    x1 = x * ratio
                    y1 = y * ratio
                    box_side = win_size * ratio

                    label = labels[idx]
                    if "dog" in label:
                        color, text = (0, 255, 255), "DOG"
                    elif "cat" in label:
                        color, text = (0, 0, 255), "CAT"
                    else:
                        continue

                    cv2.putText(image, text, (x1 + 10, y1 + 25),
                                cv2.FONT_HERSHEY_SIMPLEX, 0.7, color, 2)
                    cv2.rectangle(image, (x1 + 2, y1 + 2),
                                  (x1 + box_side - 2, y1 + box_side - 2),
                                  color, 2)


# Parametri
region_w, region_h = 1440, 720
input_image = cv2.imread("download.png")

# Ucitavanje labela
with open("synset_words.txt") as f:
    raw = f.read().strip().split("\n")
labels = [line.split(" ", 1)[-1].split(",")[0] for line in raw]

# Ucitavanje CNN modela (GoogLeNet)
net = cv2.dnn.readNetFromCaffe("bvlc_googlenet.prototxt",
                               "bvlc_googlenet.caffemodel")

# Priprema slike i detekcija
roi = crop_region(input_image, region_w, region_h)
classify_pets(roi, labels, net)

cv2.imwrite("output.jpg", roi)
print("Slika output.jpg je sacuvana.")