% Garrett Scholtes 
% Meeks, Moon, Rossi 
% 
% Look at some of the statistics regarding our
% training data

clear all; 
close all; 

daniel = xlsread('formatted/daniel.xlsx');
garrett = xlsread('formatted/garrett.xlsx');
meeks = xlsread('formatted/meeks.xlsx');
ricky = xlsread('formatted/ricky.xlsx');

% Number of data points
COUNT = size(daniel, 1); 

%% Visualize the 2-D emotion distribution for each person 
figure; 

rng(2017);

jitterx = rand(size(daniel(:,2))) - 0.5;
jittery = rand(size(daniel(:,2))) - 0.5;

subplot(1,4,1);
scatter(daniel(:,2)+jitterx,daniel(:,3)+jittery,'r.');
title('Daniel distribution');
xlabel('Positivity');
ylabel('Intensity');


subplot(1,4,2);
scatter(garrett(:,2),garrett(:,3),'b.');
title('Garrett distribution');
xlabel('Positivity');
ylabel('Intensity');

subplot(1,4,3);
scatter(meeks(:,2)+jitterx,meeks(:,3)+jittery,'m.');
title('Meeks distribution');
xlabel('Positivity');
ylabel('Intensity');

subplot(1,4,4);
scatter(ricky(:,2)+jitterx,ricky(:,3)+jittery,'k.');
title('Ricky distribution');
xlabel('Positivity');
ylabel('Intensity');


%% Visualize the distribution for each parameter per person
% Positivity
figure; 

subplot(2,2,1);
plot(sort(daniel(:,2)),'r.');
title('Daniel positivity');
xlabel('song (sorted)');
ylabel('positivity');

subplot(2,2,2);
plot(sort(garrett(:,2)),'b.');
title('Garrett positivity');
xlabel('song (sorted)');
ylabel('positivity');

subplot(2,2,3);
plot(sort(meeks(:,2)),'m.');
title('Meeks positivity');
xlabel('song (sorted)');
ylabel('positivity');

subplot(2,2,4);
plot(sort(ricky(:,2)),'k.');
title('Ricky positivity');
xlabel('song (sorted)');
ylabel('positivity');

% Intensity
figure; 

subplot(2,2,1);
plot(sort(daniel(:,3)),'r.');
title('Daniel intensity');
xlabel('song (sorted)');
ylabel('intensity');

subplot(2,2,2);
plot(sort(garrett(:,3)),'b.');
title('Garrett intensity');
xlabel('song (sorted)');
ylabel('intensity');

subplot(2,2,3);
plot(sort(meeks(:,3)),'m.');
title('Meeks intensity');
xlabel('song (sorted)');
ylabel('intensity');

subplot(2,2,4);
plot(sort(ricky(:,3)),'k.');
title('Ricky intensity');
xlabel('song (sorted)');
ylabel('intensity');

% Confidence
figure; 

subplot(2,2,1);
plot(sort(daniel(:,4)),'r.');
title('Daniel confidence');
xlabel('song (sorted)');
ylabel('confidence');

subplot(2,2,2);
plot(sort(garrett(:,4)),'b.');
title('Garrett confidence');
xlabel('song (sorted)');
ylabel('confidence');

subplot(2,2,3);
plot(sort(meeks(:,4)),'m.');
title('Meeks confidence');
xlabel('song (sorted)');
ylabel('confidence');

subplot(2,2,4);
plot(sort(ricky(:,4)),'k.');
title('Ricky confidence');
xlabel('song (sorted)');
ylabel('confidence');


%% Median values and variance 
% We are interested in comparing the amount of variance among the group
% members for each attribute for each song.  
% The above distributions appear to be similar, but that alone is not
% enough to ensure that variance among team members will be low.  
% 
% The median will serve as a decent measure of center for the "baseline" 
% scores for the team.  The reasoning behind this is that if 3 team members
% are in similar agreement on values but one is an outlier, the median will
% represent a less biased middle  

close all;

% Outlier cutoff
K = 5;

% What range to normalize to 
% I.e., 
%     Positivity => -1 to 1 
%     Intensity  => -1 to 1
%     Confidence =>  0 to 1
min1 = [-1 -1  0];
max1 = [ 1  1  1];

dan_norm = daniel;
gar_norm = garrett;
mee_norm = meeks;
ric_norm = ricky;

% Normalize the data 
dan_norm(:,2:4) = normalize(daniel(:,2:4), K, min1, max1);
gar_norm(:,2:4) = normalize(garrett(:,2:4), K, min1, max1);
mee_norm(:,2:4) = normalize(meeks(:,2:4), K, min1, max1);
ric_norm(:,2:4) = normalize(ricky(:,2:4), K, min1, max1);

% Account for outliers. Renormalize everyone as a group 
norm = [dan_norm; gar_norm; mee_norm; ric_norm];
coeff = repmat(max([abs(min(norm)) ; abs(max(norm))]), size(daniel,1),1);
dan_norm(:,2:4) = dan_norm(:,2:4)./coeff(:,2:4);
gar_norm(:,2:4) = gar_norm(:,2:4)./coeff(:,2:4);
mee_norm(:,2:4) = mee_norm(:,2:4)./coeff(:,2:4);
ric_norm(:,2:4) = ric_norm(:,2:4)./coeff(:,2:4);

% Scatterplot (to visualize integrity of the normalization)
figure;
j1x = rand(size(daniel(:,2)))/5-0.1; j1y = rand(size(daniel(:,2)))/5-0.1;
j2x = rand(size(daniel(:,2)))/5-0.1; j2y = rand(size(daniel(:,2)))/5-0.1;
j3x = rand(size(daniel(:,2)))/5-0.1; j3y = rand(size(daniel(:,2)))/5-0.1;
% The 0.95 multipler is 
plot(0.95*dan_norm(:,2)+j1x, 0.95*dan_norm(:,3)+j1y, 'r.', ...
     0.95*gar_norm(:,2), 0.95*gar_norm(:,3), 'b.', ...
     0.95*mee_norm(:,2)+j2x,0.95* mee_norm(:,3)+j2y, 'm.', ...
     0.95*ric_norm(:,2)+j3x, 0.95*ric_norm(:,3)+j3y, 'k.');
title('Scatterplot of +/- versus intensity');
xlabel('positivity');
ylabel('intensity');
legend('Daniel','Garrett','Meeks','Ricky');

% More normalization integrity checking 
figure;


subplot(3,1,1);
plot(sort(dan_norm(:,1)),dan_norm(:,2),'r.', ...
     sort(dan_norm(:,1)),dan_norm(:,2),'r.', ...
     sort(dan_norm(:,1)),dan_norm(:,2),'r.', ...
     sort(dan_norm(:,1)),dan_norm(:,2),'r.');
