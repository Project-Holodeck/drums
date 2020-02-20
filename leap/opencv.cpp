#include <opencv2/core/utility.hpp>
#include <opencv2/highgui/highgui.hpp>
#include <opencv2/imgproc.hpp>
#include <iostream>
#include <stdlib.h>

using namespace std;
using namespace cv;

int width = 0;
int height = 0;

extern "C" void track(unsigned char *);
extern "C" void track_set_dim(int, int);


void track_set_dim(int img_width, int img_height) {
	width = img_width;
	height = img_height;
}

void track(unsigned char * data) {
	Mat img(Size(width, height), CV_8UC1, data);
	//Mat parsed;
	//int kdata[] = { 1, 1,1,1,1,1,1,1,1 };
	//Mat kernel(3, 3, CV_32F, kdata);
	//morphologyEx(img, parsed, MORPH_OPEN, kernel);
	//GaussianBlur(parsed, parsed, Size(9, 9), 40, 40);
	//threshold(parsed, parsed, 120, 255, THRESH_BINARY);
	//erode(parsed, parsed, kernel, Point(-1, -1), 2, 0, morphologyDefaultBorderValue());
	//dilate(parsed, parsed, kernel, Point(-1, -1), 4, 0, morphologyDefaultBorderValue());

	double minVal, maxVal = 0;
	bool p1_det = false, p2_det = false;
	int minIdx, maxIdx = 0;
	Point minIdxpoint, parsedPoint1, parsedPoint2;
	minMaxLoc(img, &maxVal, &minVal, &minIdxpoint, &parsedPoint1);
	//memcpy(&parsedPoint1, &maxIdxpoint, sizeof(Point));
	if (parsedPoint1.x != 634 || parsedPoint1.y != 239) {
		p1_det = true;
		circle(img, parsedPoint1, 30, Scalar(0, 0, 0), FILLED, 8, 0);
	}

	minMaxLoc(img, &maxVal, &minVal, &minIdxpoint, &parsedPoint2);
	//memcpy(&parsedPoint2, &maxIdxpoint, sizeof(Point));
	if (parsedPoint2.x != 634 || parsedPoint2.y != 239) {
		p2_det = true;
		circle(img, parsedPoint2, 30, Scalar(0, 0, 0), FILLED, 8, 0);
	}

	if (p1_det) circle(img, parsedPoint1, 20, Scalar(255, 255, 255), FILLED, 8, 0);
	if (p2_det) circle(img, parsedPoint2, 20, Scalar(255, 255, 255), FILLED, 8, 0);

	if (p1_det && p2_det) fprintf(stderr, "1: (%d, %d), 2: (%d, %d)\n", parsedPoint1.x, parsedPoint1.y, parsedPoint2.x, parsedPoint2.y);
	else if (p1_det) fprintf(stderr, "1: (%d, %d)\n", parsedPoint1.x, parsedPoint1.y);
	else if (p2_det) fprintf(stderr, "2: (%d, %d)\n", parsedPoint2.x, parsedPoint2.y);


	imshow("opencv window", img);
	waitKey(1);
}