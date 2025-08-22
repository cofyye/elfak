import cv2 as cv
from cv2 import aruco
import numpy as np
import glob

# Parametri za ArUco tablu
BOARD_SIZE = (5, 7)          # broj markera po kolonama i redovima
MARKER_SIZE = 2              # sirina jednog markera (u cm)
SPACE_BETWEEN = 0.4          # razmak izmedju markera

aruco_dict = aruco.getPredefinedDictionary(aruco.DICT_6X6_1000)
detector_cfg = aruco.DetectorParameters()
aruco_board = aruco.GridBoard(BOARD_SIZE, MARKER_SIZE, SPACE_BETWEEN, aruco_dict)


def calibrate_camera(image_glob: str):
    """Kalibracija kamere pomocu ArUco table sa vise slika."""
    img_paths = glob.glob(image_glob)
    corners_all, ids_all, counts = [], [], []
    img_resolution = None

    for idx, path in enumerate(img_paths):
        image = cv.imread(path)
        gray = cv.cvtColor(image, cv.COLOR_BGR2GRAY)

        marker_corners, marker_ids, _ = aruco.detectMarkers(gray, aruco_dict, parameters=detector_cfg)

        if marker_ids is not None and len(marker_ids) > 0:
            corners_all.append(marker_corners)
            ids_all.append(marker_ids)
            counts.append(len(marker_ids))

            if img_resolution is None:
                img_resolution = gray.shape[::-1]

    if not corners_all:
        raise ValueError("Nema dovoljno detektovanih markera za kalibraciju kamere.")

    # Ravnjanje listi u jedan niz
    flat_corners = [c for subset in corners_all for c in subset]
    flat_ids = [i for subset in ids_all for i in subset]

    ids_array = np.array(flat_ids, dtype=np.int32)
    counts_array = np.array(counts)

    _, camera_matrix, distortion, _, _ = aruco.calibrateCameraAruco(
        flat_corners, ids_array, counts_array, aruco_board, img_resolution, None, None
    )

    optimal_matrix, roi = cv.getOptimalNewCameraMatrix(camera_matrix, distortion, img_resolution, 1, img_resolution)
    return camera_matrix, distortion, optimal_matrix, roi


def track_pose(video_cap, camera_matrix, distortion, optimal_matrix, roi):
    """Detekcija poze ArUco table iz video izvora."""
    while True:
        ret, frame = video_cap.read()
        if not ret:
            break

        gray_frame = cv.cvtColor(frame, cv.COLOR_BGR2GRAY)
        corners, ids, rejected = aruco.detectMarkers(gray_frame, aruco_dict, parameters=detector_cfg)
        aruco.refineDetectedMarkers(gray_frame, aruco_board, corners, ids, rejected)

        display_frame = frame.copy()

        if ids is not None and len(ids) > 0:
            aruco.drawDetectedMarkers(display_frame, corners, ids)

            ok, rvec, tvec = aruco.estimatePoseBoard(
                corners, ids, aruco_board, camera_matrix, distortion, None, None
            )
            if ok:
                cv.drawFrameAxes(display_frame, camera_matrix, distortion, rvec, tvec, 5)

        corrected = cv.undistort(display_frame, camera_matrix, distortion, None, optimal_matrix)
        resized = cv.resize(corrected, None, fx=0.6, fy=0.6, interpolation=cv.INTER_AREA)
        cv.imshow("ArUco Pose Tracking", resized)

        if cv.waitKey(1) & 0xFF == ord("q"):
            break


if __name__ == "__main__":
    calib_imgs = "Aruco/*.jpg"
    video_file = "Aruco/Aruco_board.mp4"

    cam_matrix, dist_coeffs, new_matrix, region = calibrate_camera(calib_imgs)
    video_stream = cv.VideoCapture(video_file)

    track_pose(video_stream, cam_matrix, dist_coeffs, new_matrix, region)

    video_stream.release()
    cv.destroyAllWindows()
