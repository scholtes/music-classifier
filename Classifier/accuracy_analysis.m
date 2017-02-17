clear all;

% Load in full data
annotations_with_features = xlsread('../annotations_with_features.xlsx');
dataset_size = length(annotations_with_features);

% Go through sound clip files
files = [];
for i = 1:dataset_size
   files{i} =  [num2str(annotations_with_features(i,1)) '.mp3'];
end

% Classify each song
for i = 1:dataset_size
    file = files{i};
    cmd = ['dist\emotify\emotify.exe ../clips_45seconds/' file];
    [status, output] = system(cmd);
    pos_i = strfind(output, 'positivity');
    nrg_i = strfind(output, 'energy');
    
    pos_val = output(pos_i+13:pos_i+13+5);
    pos_val = str2double(pos_val);
    
    nrg_val = output(nrg_i+9:nrg_i+9+5);
    nrg_val = str2double(nrg_val);
    
    data(i,1) = str2num(file(1:end-4));
    data(i,2) = pos_val * 10;
    data(i,3) = annotations_with_features(i,4);
    data(i,4) = annotations_with_features(i,5);
    data(i,5) = abs(data(i,3) - data(i,2)) < data(i,4);
    
    data(i,6) = nrg_val * 10;
    data(i,7) = annotations_with_features(i,2);
    data(i,8) = annotations_with_features(i,3);
    data(i,9) = abs(data(i,6) - data(i,7)) < data(i,8);
    
end

pos_accuracy = sum(data(:,5)) / (dataset_size);
nrg_accuracy = sum(data(:,9)) / (dataset_size);

