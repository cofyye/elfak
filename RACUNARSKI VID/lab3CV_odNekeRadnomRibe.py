import numpy as np
import cv2 as cv

MIN_BROJ_POKLAPANJA = 30

slika1 = cv.imread('1.JPG')
slika2 = cv.imread('2.JPG')
slika3 = cv.imread('3.JPG')

def napraviPanoramu():
    img = NapraviPanoramuOdDveSlike(slika2, slika3)
    img = NapraviPanoramuOdDveSlike(slika1, img)
    return img

def NapraviPanoramuOdDveSlike(imgL, imgR):
    detector = cv.SIFT_create() #SIFT detektor

    # Kao rezultat detectAndCompute metode dobijamo kljucne tacke(kp) i njihove odg. deskriptore
    kp1, des1 = detector.detectAndCompute(imgR, mask=None)
    kp2, des2 = detector.detectAndCompute(imgL, None)

    FLANN_INDEX_KDTREE = 1
    index_params = dict(algorithm=FLANN_INDEX_KDTREE, trees=5)
    search_params = dict(checks=50)
    flann = cv.FlannBasedMatcher(index_params, search_params)
    matches = flann.knnMatch(des1, des2, k=2) # pronalazi sva poklapanja kljucnih tacaka

    # eliminisemo lose veze
    good = []
    for m, n in matches:
        if m.distance < 0.7 * n.distance:
            good.append(m)

    if len(good) > MIN_BROJ_POKLAPANJA:
        src_pts = np.float32([kp1[m.queryIdx].pt for m in good]).reshape(-1, 1, 2)
        dst_pts = np.float32([kp2[m.trainIdx].pt for m in good]).reshape(-1, 1, 2)

        M, mask = cv.findHomography(src_pts, dst_pts, cv.RANSAC, 5.0)
    else:
        print(f'Nije pronađen dovoljan broj poklapanja :{len(good)}/{MIN_BROJ_POKLAPANJA}')
        return None
    
    width = imgL.shape[1] + imgR.shape[1] 
    height = imgL.shape[0] + int (imgR.shape[0] / 2)
    outimg = cv.warpPerspective(imgR, M, (width, height)) # sirina kao 2 slike, visina otprilike slika i po
    outimg[0:imgL.shape[0], 0:imgL.shape[1]] = imgL

    outimg = trim(outimg)
    return outimg

def trim(frame):
    if not np.sum(frame[0]):
        return trim(frame[1:])
    if not np.sum(frame[-1]):
        return trim(frame[:-2])
    if not np.sum(frame[:, 0]):
        return trim(frame[:, 1:])
    if not np.sum(frame[:, -1]):
        return trim(frame[:, :-2])
    return frame
    
panorama = napraviPanoramu()

cv.imshow("Panorama", panorama)
cv.imwrite("output.jpg", panorama)
cv.waitKey(0)